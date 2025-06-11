using System.ComponentModel.DataAnnotations;

namespace Test.Models;

public class Car
{
    public int Id { get; set; }
    
    public int CarManufacturerId { get; set; }
    
    [Required]
    [StringLength(200)]
    public string ModelName { get; set; }
    
    public int Number { get; set; }
    
    public CarManufacturer CarManufacturer { get; set; }
    
    public ICollection<Driver> Drivers { get; set; }
}