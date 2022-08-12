namespace Rebels.ExampleProject.CQRS.V1.Queries;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rebels.ExampleProject.Data;
using Rebels.ExampleProject.Dtos.V1;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;

public class GetRebelsQuery : IRequest<List<RebelDto>>
{
    [MinLength(3)]
    public string? NameFilter { get; set; } = null;

    public class GetRebelsQueryHandler : IRequestHandler<GetRebelsQuery, List<RebelDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRebelsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<RebelDto>> Handle(GetRebelsQuery request, CancellationToken cancellationToken)
        {
            var q = _unitOfWork.Rebels.AsQueryable();

            if (!string.IsNullOrEmpty(request.NameFilter))
            {
                q = q.Where(r => r.Name.Contains(request.NameFilter));
            }

            return _mapper.Map<List<RebelDto>>(await q.ToListAsync(cancellationToken));
        }
    }

}
