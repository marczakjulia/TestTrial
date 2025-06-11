using System.ComponentModel.DataAnnotations;

namespace Test.Models;

public class Competition
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    public ICollection<DriverCompetition> DriverCompetitions { get; set; }
}