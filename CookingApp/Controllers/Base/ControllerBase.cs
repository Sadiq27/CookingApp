namespace CookingApp.Controllers.Base;

using System.Net;

public abstract class ControllerBase
{
    public HttpListenerResponse? Response { get; set; }
    public HttpListenerRequest? Request { get; set; }

    protected async Task LayoutAsync(string bodyHtml, string layoutName = "layout")
    {
        Response.ContentType = "text/html";
        using var streamWriter = new StreamWriter(Response.OutputStream);

        var layoutPath = Path.Combine("Views", "Layouts", $"{layoutName}.html");
        var html = (await File.ReadAllTextAsync(layoutPath))
            .Replace("{{body}}", bodyHtml);

        await streamWriter.WriteLineAsync(html);
    }

    protected async Task WriteViewAsync(string viewPath, Dictionary<string, object>? viewValues = null)
    {
        var fullPath = Path.Combine("Views", viewPath + ".html");
        var html = await File.ReadAllTextAsync(fullPath);

        if (viewValues is not null)
        {
            foreach (var viewValue in viewValues)
            {
                html = html.Replace($"{{{{{viewValue.Key}}}}}", viewValue.Value.ToString());
            }
        }

        await LayoutAsync(html);
    }
}
