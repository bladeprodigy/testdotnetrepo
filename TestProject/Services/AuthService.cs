using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TestProject.DTOs;
using TestProject.Repositories;
using static System.DateTime;
using static System.Guid;
using static Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TestProject.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    
    public AuthService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }
    
    public async Task<(int,string)> Register(RegisterDTO registerDto)
    {
        var userExists = await _userManager.FindByNameAsync(registerDto.Username);
        if (userExists != null)
            return (0, "User already exists");

        IdentityUser user = new()
        {
            Email = registerDto.Email,
            SecurityStamp = NewGuid().ToString(),
            UserName = registerDto.Username
        };
        var createUserResult = await _userManager.CreateAsync(user, registerDto.Password);
        if (!createUserResult.Succeeded)
            return (0,"User creation failed! Please check user details and try again.");

        if (!await _roleManager.RoleExistsAsync("USER"))
            await _roleManager.CreateAsync(new IdentityRole("USER"));

        if (await _roleManager.RoleExistsAsync("USER"))
            await _userManager.AddToRoleAsync(user, "USER");

        return (1,"User created successfully!");
    }
    
    public async Task<(int,string)> Login(LoginDTO loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.Username);
        if (user == null)
            return (0, "Invalid username");
        if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            return (0, "Invalid password");
            
        var userRoles = await _userManager.GetRolesAsync(user);
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(Jti, NewGuid().ToString()),
        };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }
        var token = GenerateToken(authClaims);
        return (1, token);
    }
    
    private string GenerateToken(IEnumerable<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _configuration["JWT:ValidIssuer"],
            Audience = _configuration["JWT:ValidAudience"],
            Expires = UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}