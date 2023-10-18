using TestProject.DTOs;

namespace TestProject.Repositories;

public interface IAuthService
{
    public Task<(int, string)> Register(RegisterDTO registerDto);
    public Task<(int, string)> Login(LoginDTO loginDto);
}