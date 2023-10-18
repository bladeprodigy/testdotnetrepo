using System.ComponentModel.DataAnnotations;

namespace TestProject.DTOs;

public class RegisterDTO
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm password is required")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    public RegisterDTO(string username, string email, string password, string confirmPassword)
    {
        Username = username;
        Email = email;
        Password = password;
        ConfirmPassword = confirmPassword;
    }
    
    public RegisterDTO()
    {
    }
}