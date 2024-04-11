namespace CookingApp.Attributes.Http;

using CookingApp.Attributes.Http.Base;

public class HttpPutAttribute : HttpAttribute
{
    public HttpPutAttribute() : base("PUT")
    {
    }
}