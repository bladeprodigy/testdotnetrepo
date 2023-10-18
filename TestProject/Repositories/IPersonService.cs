using TestProject.DTOs;
using TestProject.Models;

namespace TestProject.Repositories;

public interface IPersonService
{
    Task<Person> CreatePersonAsync(PersonDTO personDto);
    Task<PersonDTO> GetPersonByIdAsync(int id);
    Task<IEnumerable<PersonDTO>> GetAllPeopleAsync();
}