namespace Test.Models.DTOs;

public class DriverDetailDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthday { get; set; }
    public int CarNumber { get; set; }
    public string ManufacturerName { get; set; }
    public string CarModelName { get; set; }
} 