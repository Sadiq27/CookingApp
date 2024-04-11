namespace CookingApp.Attributes.Http;

using CookingApp.Attributes.Http.Base;

public class HttpPostAttribute : HttpAttribute
{
    public HttpPostAttribute() : base("POST")
    {
    }
}