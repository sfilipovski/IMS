using System.ComponentModel.DataAnnotations;

namespace IMS.Domain.DTO.Command;

public class UserLoginDto
{
    [Required(ErrorMessage = "Username is required")]
    [EmailAddress(ErrorMessage = "Username email address")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}
