using System;
using System.Net;
using System.Reflection;
using System.Text;
using System.IO;
using CookingApp.Attributes.Http.Base;
using CookingApp.Controllers.Base;
using CookingApp.Controllers;

var listener = new HttpListener();
var prefix = "http://*:8080/";

listener.Prefixes.Add(prefix);

listener.Start();

Console.WriteLine($"Server started... {prefix.Replace("*", "localhost")}");

while (true)
{
    var context = await listener.GetContextAsync();
    var request = context.Request;
    var response = context.Response;
    var handled = false;

    // Обработка корневого пути с использованием HomeController и метода Index
    if (request.Url.AbsolutePath == "/" && request.HttpMethod == "GET")
    {
        var homeController = new HomeController();
        homeController.Request = request;
        homeController.Response = response;
        await homeController.Index();
        handled = true;
    }
    else
    {
        // Ваша существующая логика маршрутизации для других путей
        foreach (Type controllerType in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (controllerType.IsSubclassOf(typeof(ControllerBase)))
            {
                var controller = (ControllerBase)Activator.CreateInstance(controllerType);
                controller.Request = request;
                controller.Response = response;

                foreach (MethodInfo method in controllerType.GetMethods())
                {
                    var attributes = method.GetCustomAttributes<HttpAttribute>();
                    foreach (var attribute in attributes)
                    {
                        var expectedPath = $"/{controllerType.Name.Replace("Controller", "")}/{attribute.ActionName}";
                        if (request.HttpMethod.Equals(attribute.MethodType, StringComparison.OrdinalIgnoreCase) &&
                            expectedPath.Equals(request.Url.AbsolutePath, StringComparison.OrdinalIgnoreCase))
                        {
                            await (Task)method.Invoke(controller, Array.Empty<object>());
                            handled = true;
                            break;
                        }
                    }
                    if (handled) break;
                }
            }
            if (handled) break;
        }
    }

    if (!handled)
    {
        response.StatusCode = 404;
        using (var writer = new StreamWriter(response.OutputStream))
            await writer.WriteLineAsync("Not Found");
    }

    response.Close();
}
