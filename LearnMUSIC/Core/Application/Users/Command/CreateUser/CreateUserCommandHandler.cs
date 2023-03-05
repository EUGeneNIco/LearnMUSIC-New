using LearnMusic.Core.Domain.Enumerations;
using LearnMUSIC.Common.Common;
using LearnMUSIC.Common.Helper;
using LearnMUSIC.Core.Application._Exceptions;
using LearnMUSIC.Core.Application._Interfaces;
using LearnMUSIC.Core.Domain.Contracts;
using LearnMUSIC.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnMUSIC.Core.Application.Users.Command.CreateUser
{
  public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, long>
  {
    private readonly IAppDbContext dbContext;
    private readonly IDateTime dateTime;
    private readonly IUserRepository userRepository;

    public CreateUserCommandHandler(IAppDbContext dbContext, IDateTime dateTime, IUserRepository userRepository)
    {
      this.dbContext = dbContext;
      this.dateTime = dateTime;
      this.userRepository = userRepository;
    }

    public async Task<long> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
      var existingMemberWithSameUsername = await this.userRepository.GetUserByUsernameAsync(request.Username.Trim());

      if (existingMemberWithSameUsername != null)
      {
        throw new DuplicateException("Username already exists.");
      }

      //Add User
      var createdOn = dateTime.Now;

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

      GrantModuleAccess(user);

      await dbContext.SaveChangesAsync(cancellationToken);

      return user.Id;
    }

    public void GrantModuleAccess(User user)
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
    }
  }
}
