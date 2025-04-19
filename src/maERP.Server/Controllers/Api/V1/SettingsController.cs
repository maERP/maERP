using Asp.Versioning;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class SettingsController(ISettingsRepository settingsRepository) : ControllerBase
{
    // GET: api/v1/Settings
    [HttpGet]
    public async Task<ActionResult<Result<List<Setting>>>> GetAll()
    {
        var settings = await settingsRepository.GetAllAsync();
        return Ok(await Result<List<Setting>>.SuccessAsync(settings));
    }

    // GET api/v1/Settings/key
    [HttpGet("{key}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<Setting>>> GetByKey(string key)
    {
        var settings = await settingsRepository.GetAllAsync();
        var setting = settings.FirstOrDefault(s => s.Key == key);
        
        if (setting == null)
            return NotFound(await Result<Setting>.FailAsync("Setting not found"));
            
        return Ok(await Result<Setting>.SuccessAsync(setting));
    }

    // PUT api/v1/Settings
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<List<Setting>>>> UpdateSettings([FromBody] List<Setting> settings)
    {
        if (!settings.Any())
            return BadRequest(await Result<List<Setting>>.FailAsync("No settings provided"));

        var existingSettings = await settingsRepository.GetAllAsync();
        var updatedSettings = new List<Setting>();

        foreach (var setting in settings)
        {
            var existingSetting = existingSettings.FirstOrDefault(s => s.Key == setting.Key);
            if (existingSetting != null)
            {
                existingSetting.Value = setting.Value;
                await settingsRepository.UpdateAsync(existingSetting);
                updatedSettings.Add(existingSetting);
            }
            else
            {
                await settingsRepository.CreateAsync(setting);
                updatedSettings.Add(setting);
            }
        }

        return Ok(await Result<List<Setting>>.SuccessAsync(updatedSettings));
    }
}
