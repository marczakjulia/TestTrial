using System.ComponentModel.DataAnnotations;

namespace Test.Models.DTOs;

public class CreateDriverDto
{
    [Required]
    [StringLength(200)]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(200)]
    public string LastName { get; set; }
    
    [Required]
    public DateTime Birthday { get; set; }
    
    [Required]
    public int CarId { get; set; }
} 