using Test.Models;
using Test.Models.DTOs;

namespace Test.Services;

public interface IDriverService
{
    Task<IEnumerable<DriverListDto>> GetAllDriversAsync(string sortBy = "FirstName");
    Task<DriverDetailDto> GetDriverByIdAsync(int id);
    Task<IEnumerable<CompetitionDto>> GetDriverCompetitionsAsync(int driverId);
    Task<DriverResponseDto> CreateDriverAsync(CreateDriverDto driverDto);
    Task AssignDriverToCompetitionAsync(int driverId, int competitionId);
} 