namespace Test.Models.DTOs;

public class DriverResponseDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthday { get; set; }
    public CarBasicDto Car { get; set; }
}

public class CarBasicDto
{
    public int Id { get; set; }
    public string ModelName { get; set; }
    public int Number { get; set; }
} 