using LearnMUSIC.Core.Application.Users.Models;
using MediatR;

namespace LearnMUSIC.Core.Application.Users.Queries.GetAllUsers
{
  public class GetAllUsersQuery : IRequest<IEnumerable<UserGridItem>>
  {
  }
}
