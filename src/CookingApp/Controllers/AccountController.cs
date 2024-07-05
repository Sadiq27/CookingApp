using CookingApp.Data;
using CookingApp.DTOs;
using CookingApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CookingApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;

        public AccountController(ApplicationDbContext context, EmailService emailService)
        {
            _emailService = emailService;
            _context = context;
        }

        [HttpGet("/Account/Register", Name = "RegistrationView")]
        public IActionResult Register()
        {
            if (TempData["error"] != null)
            {
                ModelState.AddModelError("All", "Something went wrong. Please try again");
            }

            return View();
        }

        [HttpPost("/Account/Register", Name = "RegistrationEndpoint")]
        public async Task<IActionResult> Register(UserRegisterDto registrationDto)
        {
            if (!ModelState.IsValid)
            {
                return View(registrationDto);
            }

            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == registrationDto.Username);
                if (existingUser != null)
                {
                    TempData["error"] = "Username already exists!";
                    return RedirectToRoute("RegistrationView");
                }

                var newUser = new User
                {
                    Email = registrationDto.Email,
                    Username = registrationDto.Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(registrationDto.Password),
                };

                var verificationCode = _emailService.GenerateVerificationCode();
                var message = $"Your verification code is: {verificationCode}";
                await _emailService.SendEmailAsync(newUser.Email, "Email Verification", message);

                HttpContext.Session.SetString("VerificationCode", verificationCode);
                HttpContext.Session.SetString("UserEmail", newUser.Email);
                HttpContext.Session.SetString("UserName", newUser.Username);
                HttpContext.Session.SetString("UserPassword", newUser.Password);


            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToRoute("RegistrationView");
            }

            return RedirectToRoute("Verification");
        }

        [HttpGet("/Account/Login", Name = "LoginView")]
        public IActionResult Login()
        {
            var errorMessage = TempData["error"];
            if (errorMessage != null)
            {
                ModelState.AddModelError("All", errorMessage.ToString());
            }

            return View();
        }

        [HttpPost("/Account/Login", Name = "LoginEndpoint")]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username);

            if (user == null || string.IsNullOrEmpty(loginDto.Password) || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                ModelState.AddModelError(string.Empty, "Incorrect login or password!");
                return View(loginDto);
            }

            string role = user.Role;

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("/Account/Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToRoute("LoginView");
        }


        [HttpGet]
        [Route("/[controller]/[action]", Name = "Verification")]
        public IActionResult Verification()
        {
            return View();
        }


        [HttpPost]
        [Route("/api/[controller]/[action]", Name = "VerifyCode")]
        public async Task<IActionResult> VerifyCode([FromForm] string verificationCode)
        {
            var sessionCode = HttpContext.Session.GetString("VerificationCode");
            var email = HttpContext.Session.GetString("UserEmail");
            var name = HttpContext.Session.GetString("UserName");

            var password = HttpContext.Session.GetString("UserPassword");
            if (verificationCode == sessionCode)
            {
                try
                {
                    var user = new User
                    {
                        Username = name,
                        Email = email,
                        Password = password,
                        Role = "User"
                    };
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToRoute("LoginView");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return RedirectToRoute("RegistrationEndpoint");
                }
            }
            else
            {
                ModelState.AddModelError("VerificationCode", "Invalid verification code.");
                return View("Verification");
            }


        }
    }
}