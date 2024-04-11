namespace CookingApp.Controllers;

using CookingApp.Attributes.Http;
using CookingApp.Controllers.Base;

public class HomeController : ControllerBase
{
    [HttpGet]
    public async Task Index()
    {
        await WriteViewAsync("Home/index");
    }
}
