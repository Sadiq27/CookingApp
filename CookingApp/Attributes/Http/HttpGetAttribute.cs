namespace CookingApp.Attributes.Http;

using CookingApp.Attributes.Http.Base;

public class HttpGetAttribute : HttpAttribute
{
    public HttpGetAttribute() : base("GET")
    {
    }
}