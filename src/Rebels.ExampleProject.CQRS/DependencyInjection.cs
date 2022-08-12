namespace Rebels.ExampleProject.CQRS;

using Microsoft.Extensions.DependencyInjection;
using MediatR;

public static class DependencyInjection
{
    public static void AddCQRS(this IServiceCollection serviceProvider)
    {
        serviceProvider.AddMediatR(typeof(DependencyInjection).Assembly);
        serviceProvider.AddAutoMapper(typeof(DependencyInjection).Assembly);
    }
}
