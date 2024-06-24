using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text;


namespace uRADMonitorDataReceiver.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParamsController : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Post()
    {
        try
        {
            using (StreamReader reader = new(Request.Body))
            {
                var requestBody = await reader.ReadToEndAsync();

                JObject json = JObject.Parse(requestBody);
                json["timestamp"] = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

                await System.IO.File.AppendAllTextAsync("logs.txt", json.ToString() + "\n", Encoding.UTF8);
            }

            return Ok("ok");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Failed to append log: {ex.Message}");
        }
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            if (System.IO.File.Exists("logs.txt"))
            {
                var logContents = await System.IO.File.ReadAllTextAsync("logs.txt");
                return Content(logContents, "application/json");
            }
            else
            {
                return NotFound("Log file not found.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Failed to retrieve logs: {ex.Message}");
        }
    }
}