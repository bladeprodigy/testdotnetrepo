using Microsoft.AspNetCore.Mvc;
using TestProject.DTOs;
using TestProject.Repositories;

namespace TestProject.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterDTO registerDto)
    {
        var (id, token) = await _authService.Register(registerDto);
        return CreatedAtAction("Login", new { id, token });
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginDTO loginDto)
    {
        var (id, token) = await _authService.Login(loginDto);
        return Ok(new { id, token });
    }
}