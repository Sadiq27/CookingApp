namespace CookingApp.Attributes.Http;

using CookingApp.Attributes.Http.Base;

public class HttpDeleteAttribute : HttpAttribute
{
    public HttpDeleteAttribute() : base("DELETE")
    {
    }
}