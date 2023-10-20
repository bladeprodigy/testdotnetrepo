using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestProject.Database;
using TestProject.DTOs;
using TestProject.Exceptions;
using TestProject.Models;
using TestProject.Repositories;

namespace TestProject.Services;

public class PersonService : IPersonService
{
    private readonly MyDbContext _context;
    private readonly IMapper _mapper;
    
    public PersonService(MyDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Person> CreatePersonAsync(PersonDTO personDto)
    {
        var person = _mapper.Map<Person>(personDto);
        await _context.People.AddAsync(person);
        await _context.SaveChangesAsync();
        return person;
    }
    
    public async Task<PersonDTO> GetPersonByIdAsync(int id)
    {
        var person = await _context.People.FindAsync(id);

        if (person == null)
        {
            throw new PersonNotFoundException(id);
        }
        
        return _mapper.Map<PersonDTO>(person);
        //aaaaa
        //asfdfgrhrtfer
    }

    public async Task<IEnumerable<PersonDTO>> GetAllPeopleAsync()
    {
        var people = await _context.People.ToListAsync();
        return _mapper.Map<IEnumerable<PersonDTO>>(people);
    }
}