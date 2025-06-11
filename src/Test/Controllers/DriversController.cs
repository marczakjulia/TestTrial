using Microsoft.AspNetCore.Mvc;
using Test.Models;
using Test.Models.DTOs;
using Test.Services;

namespace Test.Controllers;

[ApiController]
[Route("api/drivers")]
public class DriversController : ControllerBase
{
    private readonly IDriverService _driverService;

    public DriversController(IDriverService driverService)
    {
        _driverService = driverService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DriverListDto>>> GetDrivers([FromQuery] string sortBy = "FirstName")
    {
        try
        {
            var drivers = await _driverService.GetAllDriversAsync(sortBy);
            return Ok(drivers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DriverDetailDto>> GetDriver(int id)
    {
        try
        {
            var driver = await _driverService.GetDriverByIdAsync(id);
            return Ok(driver);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("competitions")]
    public async Task<ActionResult<IEnumerable<CompetitionDto>>> GetDriverCompetitions([FromQuery] int driverId)
    {
        try
        {
            var competitions = await _driverService.GetDriverCompetitionsAsync(driverId);
            return Ok(competitions);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<DriverResponseDto>> CreateDriver(CreateDriverDto driverDto)
    {
        try
        {
            var driver = await _driverService.CreateDriverAsync(driverDto);
            return CreatedAtAction(nameof(GetDriver), new { id = driver.Id }, driver);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("competitions")]
    public async Task<IActionResult> AssignDriverToCompetition([FromQuery] int driverId, [FromQuery] int competitionId)
    {
        try
        {
            await _driverService.AssignDriverToCompetitionAsync(driverId, competitionId);
            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
} 