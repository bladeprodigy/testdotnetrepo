using AutoMapper;

namespace TestProject.Mapper;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<Models.Person, DTOs.PersonDTO>();
        CreateMap<DTOs.PersonDTO, Models.Person>();
    }
}