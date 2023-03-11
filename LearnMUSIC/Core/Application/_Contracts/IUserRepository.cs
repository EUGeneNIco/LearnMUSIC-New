using LearnMUSIC.Core.Application.Users.Command.CreateUser;
using LearnMUSIC.Core.Application.Users.Command.UpdateUserProfile;
using LearnMUSIC.Core.Domain.Entities;

namespace LearnMUSIC.Core.Domain.Contracts
{
  public interface IUserRepository
  {
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserByUsernameAsync(string username);
    Task<User> GetUserProfileByIdAsync(long id);

    Task<User> CreateUserAsync(CreateUserCommand command, CancellationToken cancellationToken);
    Task<User> UpdateUserAsync(User user, UpdateUserProfileCommand command, CancellationToken cancellationToken);
    Task<User> GiveAccessToUserAsync(User user, CancellationToken cancellationToken);
  }
}
