using Microsoft.EntityFrameworkCore;
using Test.Models;
using Test.Models.DTOs;

namespace Test.Services;

public class DriverService : IDriverService
{
    private readonly ApplicationDbContext _context;

    public DriverService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DriverListDto>> GetAllDriversAsync(string sortBy = "FirstName")
    {
        var query = _context.Driver
            .Select(d => new DriverListDto
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Birthday = d.Birthday
            });
        
        query = sortBy?.ToLower() switch
        {
            "firstname" => query.OrderBy(d => d.FirstName),
            "lastname" => query.OrderBy(d => d.LastName),
            "birthday" => query.OrderBy(d => d.Birthday),
            _ => query.OrderBy(d => d.FirstName)
        };

        return await query.ToListAsync();
    }

    public async Task<DriverDetailDto> GetDriverByIdAsync(int id)
    {
        var driver = await _context.Driver
            .Include(d => d.Car)
                .ThenInclude(c => c.CarManufacturer)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (driver == null)
            throw new KeyNotFoundException($"Driver with ID {id} not found.");

        return new DriverDetailDto
        {
            Id = driver.Id,
            FirstName = driver.FirstName,
            LastName = driver.LastName,
            Birthday = driver.Birthday,
            CarNumber = driver.Car.Number,
            ManufacturerName = driver.Car.CarManufacturer.Name,
            CarModelName = driver.Car.ModelName
        };
    }

    public async Task<IEnumerable<CompetitionDto>> GetDriverCompetitionsAsync(int driverId)
    {
        var driver = await _context.Driver
            .Include(d => d.DriverCompetitions)
                .ThenInclude(dc => dc.Competition)
            .FirstOrDefaultAsync(d => d.Id == driverId);

        if (driver == null)
            throw new KeyNotFoundException($"Driver with ID {driverId} not found.");

        return driver.DriverCompetitions.Select(dc => new CompetitionDto
        {
            Id = dc.Competition.Id,
            Name = dc.Competition.Name,
            Date = dc.Date
        });
    }

    public async Task<DriverResponseDto> CreateDriverAsync(CreateDriverDto driverDto)
    {
        var car = await _context.Car
            .FirstOrDefaultAsync(c => c.Id == driverDto.CarId);
            
        if (car == null)
            throw new KeyNotFoundException($"Car with ID {driverDto.CarId} not found.");

        var driver = new Driver
        {
            FirstName = driverDto.FirstName,
            LastName = driverDto.LastName,
            Birthday = driverDto.Birthday,
            CarId = driverDto.CarId
        };

        _context.Driver.Add(driver);
        await _context.SaveChangesAsync();

        return new DriverResponseDto
        {
            Id = driver.Id,
            FirstName = driver.FirstName,
            LastName = driver.LastName,
            Birthday = driver.Birthday,
            Car = new CarBasicDto
            {
                Id = car.Id,
                ModelName = car.ModelName,
                Number = car.Number
            }
        };
    }

    public async Task AssignDriverToCompetitionAsync(int driverId, int competitionId)
    {
        var driver = await _context.Driver.FindAsync(driverId);
        if (driver == null)
            throw new KeyNotFoundException($"Driver with ID {driverId} not found.");

        var competition = await _context.Competition.FindAsync(competitionId);
        if (competition == null)
            throw new KeyNotFoundException($"Competition with ID {competitionId} not found.");

        var driverCompetition = new DriverCompetition
        {
            DriverId = driverId,
            CompetitionId = competitionId,
            Date = DateTime.UtcNow
        };

        _context.DriverCompetition.Add(driverCompetition);
        await _context.SaveChangesAsync();
    }
} 