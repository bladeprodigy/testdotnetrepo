namespace TestProject.DTOs;

public class PersonDTO
{
    public string Username { get; set; } = "";
    public string Name { get; set; } = "";
    public string Surname { get; set; } = "";
    public int Age { get; set; }
    public bool Student { get; set; }
    
    public PersonDTO(string username, string name, string surname, int age, bool student)
    {
        Username = username;
        Name = name;
        Surname = surname;
        Age = age;
        Student = student;
    }

    public PersonDTO()
    {
    }
}