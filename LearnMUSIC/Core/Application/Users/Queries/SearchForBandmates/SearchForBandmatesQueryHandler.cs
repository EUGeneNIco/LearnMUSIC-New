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

    public SearchForBandmatesQueryHandler(IMapper mapper, IUserRepository userRepository)
    {
      this.mapper = mapper;
      this.userRepository = userRepository;
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

      return this.mapper.Map<IEnumerable<SearchForBandmatesDto>>(otherUsers);
    }
  }
}
