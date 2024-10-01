using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace SecurityIssuesWithApi.Controllers;

public class XssController : ControllerBase
{
    [HttpGet]
    public ContentResult BadSearchAPI(string searchTerm)
    {
        var results = new List<string>
        {
            // Not sanitized al vulnerable to XSS
            searchTerm + " 1",
            searchTerm + " 2",
            searchTerm + " 3"
        };
        return new ContentResult
        {
            ContentType = "text/html",
            Content = $"[\"{string.Join("\", \"", results)}\"]"
        };
    }

    [HttpGet]
    public ContentResult GoodSearchAPI(string searchTerm)
    {
        // Sanitize the search term
        var sanitizedSearchTerm = System.Web.HttpUtility.JavaScriptStringEncode(searchTerm);

        var results = new List<string>
        {
            sanitizedSearchTerm + " 1",
            sanitizedSearchTerm + " 2",
            sanitizedSearchTerm + " 3"
        };

        return new ContentResult
        {
            ContentType = "application/json",
            Content = System.Text.Json.JsonSerializer.Serialize(results)
        };
    }
}