using AutoMapper;
using LearnMUSIC.Core.Application.Users.Queries.GetAllUsers;
using LearnMUSIC.Core.Application._Interfaces;
using LearnMUSIC.Core.Application.Users.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using LearnMUSIC.Core.Application._Exceptions;

namespace LearnMUSIC.Core.Application.Users.Queries.SearchForBandmates
{
  public class SearchForBandmatesQueryHandler : IRequestHandler<SearchForBandmatesQuery, IEnumerable<SearchForBandmatesDto>>
  {
    private readonly IAppDbContext dbContext;
    private readonly IMapper mapper;

    public SearchForBandmatesQueryHandler(IAppDbContext dbContext, IMapper mapper)
    {
      this.dbContext = dbContext;
      this.mapper = mapper;
    }

    public async Task<IEnumerable<SearchForBandmatesDto>> Handle(SearchForBandmatesQuery request, CancellationToken cancellationToken)
    {
      var loggedInUser = await this.dbContext.Users.FindAsync(request.UserId);

      if(loggedInUser is null)
      {
        throw new NotFoundException("Logged in user not found.");
      }
      if (loggedInUser.IsDeleted)
      {
        throw new AlreadyDeletedException("Logged in user already deleted.");
      }

      var users = this.dbContext.Users
        .Where(x => x.Id != loggedInUser.Id && !x.IsDeleted)
        .Include(x => x.Photos.Where(x => x.IsMain));

      return this.mapper.Map<IEnumerable<SearchForBandmatesDto>>(await users.ToListAsync(cancellationToken));
    }
  }
}
