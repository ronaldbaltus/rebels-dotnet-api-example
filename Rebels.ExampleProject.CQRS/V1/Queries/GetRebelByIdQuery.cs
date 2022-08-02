namespace Rebels.ExampleProject.CQRS.V1.Queries;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rebels.ExampleProject.Data;
using Rebels.ExampleProject.Dtos.V1;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using CSharpFunctionalExtensions;

public class GetRebelByIdQuery : IRequest<Result<RebelDto>>
{
    [Required]
    public Guid Id { get; set; }

    public class GetRebelByIdQueryHandler : IRequestHandler<GetRebelByIdQuery, Result<RebelDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRebelByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<RebelDto>> Handle(GetRebelByIdQuery request, CancellationToken cancellationToken)
        {
            var rebel = await _unitOfWork.Rebels.FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (rebel == null)
            {
                return Result.Failure<RebelDto>($"Rebel {request.Id} not found");
            }

            return Result.Success(_mapper.Map<RebelDto>(rebel));
        }
    }

}
