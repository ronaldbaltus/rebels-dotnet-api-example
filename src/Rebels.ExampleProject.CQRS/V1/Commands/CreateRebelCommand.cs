namespace Rebels.ExampleProject.CQRS.V1.Commands;
using MediatR;
using CSharpFunctionalExtensions;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using AutoMapper;
using Rebels.ExampleProject.Data;

public class CreateRebelCommand : IRequest<Result>
{
    [Required(ErrorMessage = "The name is required.")]
    [MinLength(4)]
    public string Name { get; set; } = string.Empty;

    public class CreateRebelCommandHandler : IRequestHandler<CreateRebelCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRebelCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateRebelCommand request, CancellationToken cancellationToken)
        {
            _ = await _unitOfWork.Rebels.CreateAsync(new Data.Entities.RebelEntity()
            {
                Name = request.Name,
            }, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
