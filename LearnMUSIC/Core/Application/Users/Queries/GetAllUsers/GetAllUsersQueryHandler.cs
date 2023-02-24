using AutoMapper;
using LearnMUSIC.Core.Application.Users.Queries.GetAllUsers;
using LearnMUSIC.Core.Application._Interfaces;
using LearnMUSIC.Core.Application.Users.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnMUSIC.Core.Application.Users.Queries.GetAllUsers
{
  public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserGridItem>>
  {
    private readonly IAppDbContext dbContext;
    private readonly IMapper mapper;

    public GetAllUsersQueryHandler(IAppDbContext dbContext, IMapper mapper)
    {
      this.dbContext = dbContext;
      this.mapper = mapper;
    }

    public async Task<IEnumerable<UserGridItem>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
      var users = await this.dbContext.Users
        .Where(x => x.UserName != "eugene")
        .ToListAsync(cancellationToken);

      return this.mapper.Map<IEnumerable<UserGridItem>>(users);
    }
  }
}
