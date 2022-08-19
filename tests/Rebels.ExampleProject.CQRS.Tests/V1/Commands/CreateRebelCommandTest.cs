using Rebels.ExampleProject.CQRS.V1.Commands;

namespace Rebels.ExampleProject.CQRS.Tests.V1.Commands;

public class CreateRebelCommandTest : BaseUnitTest
{
    [Fact]
    public async Task Handle_Success()
    {
        var handler = new CreateRebelCommand.CreateRebelCommandHandler(
            UnitOfWork
        );

        Assert.False(UnitOfWork.Rebels.Any(re => re.Name == "Billy Bob"));

        var newRebel = await handler.Handle(new CreateRebelCommand()
        {
            Name = "Billy Bob"
        }, CancellationToken.None);

        Assert.True(newRebel.IsSuccess);
        Assert.True(UnitOfWork.Rebels.Any(re => re.Name == "Billy Bob"));
    }
}
