namespace TestProject.Exceptions;

public class UserRegistrationFailedException : Exception
{
    public UserRegistrationFailedException(string username) : base($"User with username {username} already exists.")
    {
    }
}