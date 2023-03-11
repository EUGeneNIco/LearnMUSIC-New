using LearnMUSIC.Core.Application.Users.Models;
using MediatR;

namespace LearnMUSIC.Core.Application.Users.Command.UpdateUserProfile
{
  public class UpdateUserProfileCommand : IRequest<long>
  {
    public long Id { get; set; }

    public string UserName { get; set; }

    public string CodeName { get; set; }

    public string Bio { get; set; }

    public string AboutMe { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
  }
}
