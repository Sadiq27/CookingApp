namespace CookingApp.Extensions;

using System.Text;
using CookingApp.Models;

public static class HtmlExtensions
{
    public static string AsHtml(this IEnumerable<Recipe> recipes)
    {
        var sb = new StringBuilder();
        
         sb.Append(@"
            <style>
                body {
                    font-family: Arial, sans-serif;
                    background-color: #f8f8f8;
                }

                div.recipe {
                    background-color: #ffffff;
                    padding: 20px;
                    margin-top: 25px;
                    margin-bottom: 20px;
                    border-radius: 8px;
                    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
                }

                h2 {
                    color: #333;
                    margin-top: 0;
                }

                p {
                    color: #666;
                    line-height: 1.6;
                }

                ul {
                    margin: 10px 0;
                    padding-left: 20px;
                }

                li {
                    margin-bottom: 5px;
                }
                .btn {
                    text-decoration: none;
                    color: #333;
                    padding: 10px;
                    border: 2px solid black;
                    border-radius: 14px;
                    background-color: #666;
                    color: #fff;
                }

                p.strong {
                    color: #333;
                    font-weight: bold;
                }

                hr {
                    border: 0;
                    height: 1px;
                    background-color: #eaeaea;
                    margin: 20px 0;
                }
            </style>
            ");

        foreach (var recipe in recipes)
        {
            sb.Append("<div class=\"recipe\">");
            sb.Append($"<h2>{recipe.Name}</h2>");
            sb.Append($"<p><strong>Category:</strong> {recipe.Category}</p>");

            sb.Append("<p><strong>Ingredients:</strong><ul>");
            foreach (var ingredient in recipe.Ingredients)
            {
                sb.Append($"<li>{ingredient}</li>");
            }
            sb.Append("</ul></p>");

            sb.Append($"<p><strong>Instructions:</strong><br>{recipe.Instructions.Replace("\n", "<br>")}</p>");
            sb.Append("</div><hr>");
        }

        return sb.ToString();
    }
}
