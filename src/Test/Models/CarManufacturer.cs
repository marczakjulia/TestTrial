using System.ComponentModel.DataAnnotations;

namespace Test.Models;

public class CarManufacturer
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; }
    
    public ICollection<Car> Cars { get; set; }
}
