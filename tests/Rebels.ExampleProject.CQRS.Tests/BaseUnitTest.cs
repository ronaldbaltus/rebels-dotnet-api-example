namespace Rebels.ExampleProject.CQRS.Tests;
using Rebels.ExampleProject.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

public abstract class BaseUnitTest
{
    public IMapper MapperMock;

    public UnitOfWork UnitOfWork { get; set; }

    public BaseUnitTest()
    {
        var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new CQRS.V1.MappingProfile()));

        MapperMock = mockMapper.CreateMapper();

        var sqlLiteConnection = new SqliteConnection("Filename=:memory:");
        sqlLiteConnection.Open();

        UnitOfWork = new UnitOfWork(new DbContextOptionsBuilder<UnitOfWork>().UseSqlite(sqlLiteConnection).Options);
        UnitOfWork.Database.EnsureCreated();
    }

    public void SeedData()
    {

    }
}