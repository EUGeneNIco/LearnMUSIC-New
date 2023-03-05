using AutoMapper;
using LearnMUSIC.Core.Application._Interfaces;
using LearnMUSIC.Core.Domain.Contracts;
using LearnMUSIC.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearnMUSIC.Infrastructure.Persistence
{
  public class UserRepository : IUserRepository
  {
    private readonly IAppDbContext dbContext;

    public UserRepository(IAppDbContext dbContext)
    {
      this.dbContext = dbContext;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
      const string adminName = "eugene";

      return await this.dbContext.Users.Where(x => x.UserName != adminName && !x.IsDeleted).ToListAsync();
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
      return await this.dbContext.Users
        .FirstOrDefaultAsync(x => x.UserName == username.Trim() && !x.IsDeleted);
    }

    public async Task<User> GetUserProfileByIdAsync(long id)
    {
      return await this.dbContext.Users
        .Include(x => x.Photos.Where(x => x.IsMain))
        .SingleOrDefaultAsync(x => x.Id == id);
    }
  }
}
