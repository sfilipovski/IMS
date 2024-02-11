using System.ComponentModel.DataAnnotations;

namespace IMS.Domain.DTO.Command;

public class UserRegistrationDto
{
    [Required(ErrorMessage = "First Name is required")]
    [StringLength(100)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required")]
    [StringLength(100)]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Address is required")]
    [StringLength(100)]
    public string Address { get; set; }
    [Required(ErrorMessage = "Country is required")]
    [StringLength(100)]
    public string Country { get; set; }
    [Required(ErrorMessage = "City is required")]
    [StringLength(100)]
    public string City { get; set; }
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
