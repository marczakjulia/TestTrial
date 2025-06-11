using System.ComponentModel.DataAnnotations;

namespace Test.Models;

public class Driver
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(200)]
    public string LastName { get; set; }
    
    [Required]
    public DateTime Birthday { get; set; }
    
    public int CarId { get; set; }
    public Car Car { get; set; }
    
    public ICollection<DriverCompetition> DriverCompetitions { get; set; }
}
