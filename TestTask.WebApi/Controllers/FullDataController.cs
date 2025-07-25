using Microsoft.AspNetCore.Mvc;
using TestTask.Logic.Interfaces;
using TestTask.Logic.Services;

namespace TestTask.WebApi.Controllers;

public class FullDataController : Controller
{
    IFullDataService _fullDataService;

    public FullDataController(IFullDataService fullDataService)
    {
        _fullDataService = fullDataService;
    }
    
    [HttpGet("api/fields")]
    public async Task<IActionResult> GetFullFieldsData()
    {
        var data = await _fullDataService.GetAllFullFieldsDataAsync();
        return Ok(data);
    }
    [HttpGet("api/field/{id}/size")]
    public async Task<IActionResult> GetFieldSize(int id)
    {
        var data = await _fullDataService.GetFieldSizeDataAsync(id);
        return Ok(data);
    }
    [HttpGet("api/field/{id}/distance")]
    public async Task<IActionResult> GetDistance(int id, [FromQuery]double latitude, [FromQuery]double longitude)
    {
        var data = await _fullDataService.GetDistanceToCenterAsync(id, latitude, longitude);
        return Ok(data);
    }
    [HttpGet("api/fields/containing")]
    public async Task<IActionResult> GetContainingField([FromQuery]double latitude, [FromQuery]double longitude)
    {
        var data = await _fullDataService.GetFieldByPointDataAsync(latitude, longitude);
        return Ok(data);
    }
}