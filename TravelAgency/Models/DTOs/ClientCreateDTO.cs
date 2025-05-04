using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models.DTOs;

public class ClientCreateDTO
{
    [Length(1, 120)]
    public required string FirstName { get; set; }
    [Length(1, 120)]
    public required string LastName { get; set; }
    [Length(1, 120)]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public required string Email { get; set; }
    [Length(1, 120)]
    [Phone(ErrorMessage = "Invalid phone number format")]
    public required string Telephone { get; set; }
    [Length(1, 120)]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "Invalid PESEL format")]
    public required string Pesel { get; set; }
}
