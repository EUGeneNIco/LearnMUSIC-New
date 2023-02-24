using AutoMapper;
using LearnMUSIC.Core.Application._Exceptions;
using LearnMUSIC.Core.Application._Interfaces;
using LearnMUSIC.Core.Application.Users.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnMUSIC.Core.Application.Users.Queries.GetUserProfile
{
  public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileDto>
  {
    private readonly IAppDbContext dbContext;
    private readonly IMapper mapper;

    public GetUserProfileQueryHandler(IAppDbContext dbContext, IMapper mapper)
    {
      this.dbContext = dbContext;
      this.mapper = mapper;
    }

    public async Task<UserProfileDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
      var userQuery = await this.dbContext.Users
        .Include(x => x.Photos.Where(x => x.IsMain))
        .SingleOrDefaultAsync(x => x.Id == request.UserId);

      if(userQuery is null)
      {
        throw new NotFoundException("User not found.");
      }

      if (userQuery.IsDeleted)
      {
        throw new AlreadyDeletedException("User is deleted.");
      }

      return this.mapper.Map<UserProfileDto>(userQuery); 
    }
  }
}
