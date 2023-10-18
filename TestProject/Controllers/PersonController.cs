using Microsoft.AspNetCore.Mvc;
using TestProject.DTOs;
using TestProject.Models;
using TestProject.Repositories; 

namespace TestProject.Controllers;

[ApiController]
[Route("api/people")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;
    
    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpPost]
    public async Task<ActionResult<Person>> CreatePersonAsync([FromBody] PersonDTO personDto)
    {
        var createdPerson = await _personService.CreatePersonAsync(personDto);
        return CreatedAtAction("GetPersonById" , new { id = createdPerson.Id}, createdPerson);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<PersonDTO>> GetPersonByIdAsync(int id)
    {
        var person = await _personService.GetPersonByIdAsync(id);
        return Ok(person);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonDTO>>> GetPeople()
    {
        var people = await _personService.GetAllPeopleAsync();
        return Ok(people);
    }
}