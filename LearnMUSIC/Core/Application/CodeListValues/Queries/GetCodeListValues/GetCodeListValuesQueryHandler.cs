using AutoMapper;
using LearnMUSIC.Core.Application._Exceptions;
using LearnMUSIC.Core.Application._Interfaces;
using LearnMUSIC.Core.Application.CodeListValues.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnMUSIC.Core.Application.CodeListValues.Queries.GetCodeListValues
{
  public class GetCodeListValuesQueryHandler : IRequestHandler<GetCodeListValuesQuery, IEnumerable<CodeListValueDto>>
    {
        private readonly IAppDbContext dbContext;
        private readonly IMapper mapper;

        public GetCodeListValuesQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CodeListValueDto>> Handle(GetCodeListValuesQuery request, CancellationToken cancellationToken)
        {
            var query = this.dbContext.CodeListValues
                .Where(p => p.Type == request.Type)
                .OrderBy(x => x.Name);

            if(query is null)
            {
                throw new NotFoundException("List not found.");
            }

            return this.mapper.Map<IEnumerable<CodeListValueDto>>(await query.ToListAsync(cancellationToken));
        }
    }
}
