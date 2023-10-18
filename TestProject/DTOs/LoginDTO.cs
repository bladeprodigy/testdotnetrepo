using System.ComponentModel.DataAnnotations;

namespace TestProject.DTOs;

public class LoginDTO
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
    
    public LoginDTO(string username, string password)
    {
        Username = username;
        Password = password;
    }
    
    public LoginDTO()
    {
    }
}