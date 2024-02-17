using System.ComponentModel.DataAnnotations;

namespace IMS.Domain.DTO.Command;

public class AdminRegistrationDto
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(100)]
    public string Username { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email address")]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm password is required")]
    [Compare("Password", ErrorMessage = "The Password and Confirm Password do not match.")]
    public string ConfirmPassword { get; set; }
}
