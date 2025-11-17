using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;

namespace ProjectShop.Server.WebAPI.Controllers
{
    [ApiController]
    [Route("api/platform-rules")]
    public class PlatformRulesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetRules()
        {
            var rulesPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName, "PlatformRules/platform-rules.json");
            if (!System.IO.File.Exists(rulesPath))
                return NotFound(new { error = "platform-rules.json not found" });

            var json = System.IO.File.ReadAllText(rulesPath);
            // Allow comments in JSON
            var options = new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip };
            var doc = JsonDocument.Parse(json, options);
            return Ok(doc.RootElement.Clone());
        }
    }
}
