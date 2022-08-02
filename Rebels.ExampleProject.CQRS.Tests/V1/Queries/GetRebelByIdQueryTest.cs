using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rebels.ExampleProject.CQRS.V1.Queries;
using Xunit;

namespace Rebels.ExampleProject.CQRS.Tests.V1.Queries;

public class GetRebelByIdQueryTest : BaseUnitTest
{
    [Fact]
    public async Task Handle_Success()
    {
        var handler = new GetRebelByIdQuery.GetRebelByIdQueryHandler(
            UnitOfWorkMock,
            MapperMock
        );

        var rnd = new Random();
        var re = UnitOfWorkMock.Rebels.Skip(rnd.Next(UnitOfWorkMock.Rebels.Count() - 1)).First();

        var result = await handler.Handle(new GetRebelByIdQuery()
        {
            Id = re.Id,
        }, CancellationToken.None);


        Assert.True(result.IsSuccess);
        Assert.Equal(re.Id, result.Value.Id);
        Assert.Equal(re.Name, result.Value.Name);
    }

    [Fact]
    public async Task Handle_Failure()
    {
        var handler = new GetRebelByIdQuery.GetRebelByIdQueryHandler(
            UnitOfWorkMock,
            MapperMock
        );

        var result = await handler.Handle(new GetRebelByIdQuery()
        {
            Id = Guid.NewGuid(),
        }, CancellationToken.None);


        Assert.False(result.IsSuccess);
    }
}
