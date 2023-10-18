namespace TestProject.Models;

public class Person
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public bool Student { get; set; }
    
    public Person(string username, string name, string surname, int age, bool student)
    {
        Username = username;
        Name = name;
        Surname = surname;
        Age = age;
        Student = student;
    }
    
    public Person()
    {
    }
}

