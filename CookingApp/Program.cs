using System;
using System.Net;
using System.Reflection;
using CookingApp.Attributes.Http.Base;
using CookingApp.Controllers.Base;

var listener = new HttpListener();
var prexif = "http://*:8080/";

listener.Prefixes.Add(prexif);

listener.Start();

System.Console.WriteLine($"Server started... {prexif.Replace("*", "localhost")}");

while (true)
{
    var context = await listener.GetContextAsync();
    var request = context.Request;
    var response = context.Response;
    var handled = false;

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
                    if (request.HttpMethod.Equals(attribute.MethodType, StringComparison.OrdinalIgnoreCase) &&
                        ("/" + attribute.ActionName).Equals(request.Url.AbsolutePath, StringComparison.OrdinalIgnoreCase))
                    {
                        await (Task)method.Invoke(controller, null);
                        handled = true;
                        break;
                    }
                }
                if (handled) break;
            }
        }
        if (handled) break;
    }

    if (!handled)
    {
        response.StatusCode = 404;
        using (var writer = new StreamWriter(response.OutputStream))
            await writer.WriteLineAsync("Not Found");
    }

    response.Close();
}
