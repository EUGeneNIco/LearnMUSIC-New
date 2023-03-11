using AutoMapper;
using LearnMusic.Core.Domain.Enumerations;
using LearnMUSIC.Common.Common;
using LearnMUSIC.Common.Helper;
using LearnMUSIC.Core.Application._Interfaces;
using LearnMUSIC.Core.Application.Users.Command.CreateUser;
using LearnMUSIC.Core.Application.Users.Command.UpdateUserProfile;
using LearnMUSIC.Core.Domain.Contracts;
using LearnMUSIC.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearnMUSIC.Infrastructure.Persistence
{
  public class UserRepository : IUserRepository
  {
    private readonly IAppDbContext dbContext;
    private readonly IDateTime dateTime;

    public UserRepository(IAppDbContext dbContext, IDateTime dateTime)
    {
      this.dbContext = dbContext;
      this.dateTime = dateTime;
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

    public async Task<User> CreateUserAsync(CreateUserCommand request, CancellationToken cancellationToken)
    {
      var createdOn = this.dateTime.Now;

      var user = new User
      {
        UserName = request.Username,
        PasswordHash = PasswordHelper.Hash(request.Password),

        FirstName = request.FirstName.Trim(),
        LastName = request.LastName.Trim(),
        Email = request.Email.Trim(),
        CodeName = String.Empty,
        Bio = String.Empty,
        AboutMe = String.Empty,

        CreatedOn = createdOn,
      };

      dbContext.Users.Add(user);

      await dbContext.SaveChangesAsync(cancellationToken);

      return user;
    }

    public async Task<User> GiveAccessToUserAsync(User user, CancellationToken cancellationToken)
    {
      var modules = dbContext.Modules
        .Where(x => x.Category == ModuleCategory.Usual)
        .ToList();

      //Add User Module Access
      foreach (var module in modules)
      {
        var moduleAccess = new UserModuleAccess
        {
          User = user,
          ModuleId = module.Id,
          HasAccess = true,
        };

        dbContext.UserModuleAccesses.Add(moduleAccess);
      }

      await dbContext.SaveChangesAsync(cancellationToken);

      return user;
    }

    public async Task<User> UpdateUserAsync(User user, UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
      user.CodeName = request.CodeName.Trim();
      user.Bio = request.Bio.Trim();
      user.AboutMe = request.AboutMe.Trim();
      user.FirstName = request.FirstName.Trim();
      user.LastName = request.LastName.Trim();

      user.ModifiedOn = this.dateTime.Now;

      await this.dbContext.SaveChangesAsync(cancellationToken);

      return user;
    }
  }
}
