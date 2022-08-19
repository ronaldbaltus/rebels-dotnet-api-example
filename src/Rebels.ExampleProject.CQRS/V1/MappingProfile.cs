namespace Rebels.ExampleProject.CQRS.V1;

using Data.Entities;
using Dtos.V1;
using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RebelEntity, RebelDto>();
    }
}
