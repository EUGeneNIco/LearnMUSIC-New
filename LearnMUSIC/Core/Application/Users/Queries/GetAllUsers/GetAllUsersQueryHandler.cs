using AutoMapper;
using LearnMUSIC.Core.Application.Users.Queries.GetAllUsers;
using LearnMUSIC.Core.Application._Interfaces;
using LearnMUSIC.Core.Application.Users.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using LearnMUSIC.Core.Domain.Contracts;

namespace LearnMUSIC.Core.Application.Users.Queries.GetAllUsers
{
  public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserGridItem>>
  {
    private readonly IMapper mapper;
    private readonly IUserRepository userRepository;

    public GetAllUsersQueryHandler(IMapper mapper, IUserRepository userRepository)
    {
      this.mapper = mapper;
      this.userRepository = userRepository;
    }

    public async Task<IEnumerable<UserGridItem>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
      var users = await this.userRepository.GetAllUsersAsync();

      //if (!users.Any())
      //{
      //  return Enumerable.Empty<UserGridItem>();
      //}

      return this.mapper.Map<IEnumerable<UserGridItem>>(users);
    }
  }
}
