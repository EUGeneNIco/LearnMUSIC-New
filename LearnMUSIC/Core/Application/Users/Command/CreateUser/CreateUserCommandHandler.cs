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

      var user = await this.userRepository.CreateUserAsync(request, cancellationToken);

      await this.userRepository.GiveAccessToUserAsync(user, cancellationToken);

      return user.Id;
    }
  }
}
