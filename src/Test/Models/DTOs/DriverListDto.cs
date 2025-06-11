namespace Test.Models.DTOs;

public class DriverListDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthday { get; set; }
}

public class CarInfoDto
{
    public int Id { get; set; }
    public int Number { get; set; }
    public string ModelName { get; set; }
    public string ManufacturerName { get; set; }
} 