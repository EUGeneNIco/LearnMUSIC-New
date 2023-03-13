using AutoMapper;
using LearnMUSIC.Core.Application.Users.Queries.GetAllUsers;
using LearnMUSIC.Core.Application._Interfaces;
using LearnMUSIC.Core.Application.Users.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using LearnMUSIC.Core.Application._Exceptions;
using LearnMUSIC.Core.Domain.Contracts;

namespace LearnMUSIC.Core.Application.Users.Queries.SearchForBandmates
{
  public class SearchForBandmatesQueryHandler : IRequestHandler<SearchForBandmatesQuery, IEnumerable<SearchForBandmatesDto>>
  {
    private readonly IMapper mapper;
    private readonly IUserRepository userRepository;
    private readonly IAppDbContext dbContext;

    public SearchForBandmatesQueryHandler(IMapper mapper, IUserRepository userRepository, IAppDbContext dbContext)
    {
      this.mapper = mapper;
      this.userRepository = userRepository;
      this.dbContext = dbContext;
    }

    public async Task<IEnumerable<SearchForBandmatesDto>> Handle(SearchForBandmatesQuery request, CancellationToken cancellationToken)
    {
      var loggedInUser = await this.userRepository.GetUserProfileByIdAsync(request.UserId);

      if(loggedInUser is null)
      {
        throw new NotFoundException("Logged in user not found.");
      }
      if (loggedInUser.IsDeleted)
      {
        throw new AlreadyDeletedException("Logged in user already deleted.");
      }


      

      var users = await this.userRepository.GetAllUsersAsync();

      var otherUsers = users.Where(x => x.Id != loggedInUser.Id);

      if (request.GenreId != null && request.GenreId > 0)
      {
        var genre = await this.dbContext.CodeListValues.FindAsync(request.GenreId);

        if (genre is null)
        {
          throw new NotFoundException("Genre not found.");
        }

        otherUsers = otherUsers;
      }

      if (request.InstrumentId != null && request.GenreId > 0)
      {
        var genre = await this.dbContext.CodeListValues.FindAsync(request.GenreId);

        if (genre is null)
        {
          throw new NotFoundException("Genre not found.");
        }

        otherUsers = otherUsers;
      }

      if (!string.IsNullOrWhiteSpace(request.Name))
      {
        otherUsers = otherUsers
          .Where(x => x.CodeName.ToUpper().Contains(request.Name.ToUpper().Trim())
                  || x.FirstName.ToUpper().Contains(request.Name.ToUpper().Trim())
                  || x.LastName.ToUpper().Contains(request.Name.ToUpper().Trim()));
      }

      return this.mapper.Map<IEnumerable<SearchForBandmatesDto>>(otherUsers);
    }
  }
}
