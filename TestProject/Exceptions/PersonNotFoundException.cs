namespace TestProject.Exceptions;

public class PersonNotFoundException : Exception
{
    public PersonNotFoundException(int id) : base($"Person with id {id} was not found.")
    {
    }
}