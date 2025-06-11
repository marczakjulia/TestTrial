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
    public async Task<IActionResult> GetDrivers([FromQuery] string sortBy = "FirstName")
    {
        if (!string.IsNullOrEmpty(sortBy))
        {
            var allowedSortFields = new[] { "firstname", "lastname", "birthday" };
            if (!allowedSortFields.Contains(sortBy.ToLower()))
            {
                return BadRequest($"Invalid sort parameter. Allowed values are: FirstName, LastName, Birthday");
            }
        }

        try
        {
            var drivers = await _driverService.GetAllDriversAsync(sortBy);
            return Ok(drivers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Internal server error", message = ex.Message });
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDriver(int id)
    {
        if (id <= 0)
        {
            return BadRequest(new { error = "Invalid ID", message = "ID must be greater than 0" });
        }

        try
        {
            var driver = await _driverService.GetDriverByIdAsync(id);
            return Ok(driver);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = "Not Found", message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Internal server error", message = ex.Message });
        }
    }
    
    [HttpGet("competitions")]
    public async Task<IActionResult> GetDriverCompetitions([FromQuery] int driverId)
    {
        if (driverId <= 0)
        {
            return BadRequest(new { error = "Invalid ID", message = "Driver ID must be greater than 0" });
        }

        try
        {
            var competitions = await _driverService.GetDriverCompetitionsAsync(driverId);
            return Ok(competitions);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = "Not Found", message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Internal server error", message = ex.Message });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateDriver([FromBody] CreateDriverDto driverDto)
    {
        if (driverDto == null)
        {
            return BadRequest(new { error = "Invalid Input", message = "Request body cannot be empty" });
        }

        if (driverDto.CarId <= 0)
        {
            return BadRequest(new { error = "Invalid Input", message = "Car ID must be greater than 0" });
        }

        if (driverDto.Birthday >= DateTime.UtcNow)
        {
            return BadRequest(new { error = "Invalid Input", message = "Birthday cannot be in the future" });
        }

        if (string.IsNullOrWhiteSpace(driverDto.FirstName) || string.IsNullOrWhiteSpace(driverDto.LastName))
        {
            return BadRequest(new { error = "Invalid Input", message = "First name and last name are required" });
        }

        try
        {
            var driver = await _driverService.CreateDriverAsync(driverDto);
            return CreatedAtAction(nameof(GetDriver), new { id = driver.Id }, driver);
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(new { error = "Invalid Input", message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Internal server error", message = ex.Message });
        }
    }
    
    [HttpPost("competitions")]
    public async Task<IActionResult> AssignDriverToCompetition([FromQuery] int driverId, [FromQuery] int competitionId)
    {
        if (driverId <= 0)
        {
            return BadRequest(new { error = "Invalid Input", message = "Driver ID must be greater than 0" });
        }

        if (competitionId <= 0)
        {
            return BadRequest(new { error = "Invalid Input", message = "Competition ID must be greater than 0" });
        }

        try
        {
            await _driverService.AssignDriverToCompetitionAsync(driverId, competitionId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = "Not Found", message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Internal server error", message = ex.Message });
        }
    }
} 