using AutoMapper;
using LearnMUSIC.Core.Application._Exceptions;
using LearnMUSIC.Core.Application._Interfaces;
using LearnMUSIC.Core.Application.Users.Models;
using LearnMUSIC.Core.Domain.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnMUSIC.Core.Application.Users.Queries.GetUserProfile
{
  public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileDto>
  {
    private readonly IMapper mapper;
    private readonly IUserRepository userRepository;

    public GetUserProfileQueryHandler(IMapper mapper, IUserRepository userRepository)
    {
      this.mapper = mapper;
      this.userRepository = userRepository;
    }

    public async Task<UserProfileDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
      var userQuery = await this.userRepository.GetUserProfileByIdAsync(request.UserId);

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
