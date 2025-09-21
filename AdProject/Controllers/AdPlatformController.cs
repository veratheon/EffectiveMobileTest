using AdProject.Entities;
using AdProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdProject.Controllers;

[ApiController]
[Route("[controller]")]
public class AdPlatformController : ControllerBase
{
    private readonly IAdPlatformService _service;

    public AdPlatformController(IAdPlatformService service)
    {
        _service = service;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadPlatforms(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Файл не загружен или пустой");

        using var stream = file.OpenReadStream();
        using var reader = new StreamReader(stream);
        var content = await reader.ReadToEndAsync();

        await _service.AddPlatforms(content);

        return Ok("Данные загружены успешно");
    }

    [HttpGet("")]
    public async Task<IEnumerable<AdPlatform>> GetPlatforms(string location)
    {
        var result = await _service.FindPlatforms(location);
        return result;
    }
}

