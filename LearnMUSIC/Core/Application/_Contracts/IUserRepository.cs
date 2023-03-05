using LearnMUSIC.Core.Domain.Entities;

namespace LearnMUSIC.Core.Domain.Contracts
{
  public interface IUserRepository
  {
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserByUsernameAsync(string username);
    Task<User> GetUserProfileByIdAsync(long id);
  }
}
