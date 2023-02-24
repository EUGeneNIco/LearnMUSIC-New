using LearnMUSIC.Core.Application.Users.Models;
using MediatR;

namespace LearnMUSIC.Core.Application.Users.Queries.GetUserProfile
{
  public class GetUserProfileQuery : IRequest<UserProfileDto>
  {
    public long UserId { get; set; }
  }
}
