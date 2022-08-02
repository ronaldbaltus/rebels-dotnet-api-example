namespace Rebels.ExampleProject.CQRS.Tests;
using AutoMapper;

public abstract class BaseUnitTest
{
    public Mocks.UnitOfWorkMock UnitOfWorkMock;
    public IMapper MapperMock;

    public BaseUnitTest()
    {
        UnitOfWorkMock = new Mocks.UnitOfWorkMock();

        var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new CQRS.V1.MappingProfile()));

        MapperMock = mockMapper.CreateMapper();
    }
}