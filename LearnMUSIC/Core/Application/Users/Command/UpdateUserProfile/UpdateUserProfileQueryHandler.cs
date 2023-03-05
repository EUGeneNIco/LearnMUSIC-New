using AutoMapper;
using LearnMUSIC.Common.Common;
using LearnMUSIC.Core.Application._Exceptions;
using LearnMUSIC.Core.Application._Interfaces;
using LearnMUSIC.Core.Application.Users.Models;
using LearnMUSIC.Core.Domain.Contracts;
using MediatR;

namespace LearnMUSIC.Core.Application.Users.Command.UpdateUserProfile
{
  public class UpdateUserProfileQueryHandler : IRequestHandler<UpdateUserProfileQuery, long>
  {
    private readonly IAppDbContext dbContext;
    private readonly IDateTime dateTime;
    private readonly IUserRepository userRepository;

    public UpdateUserProfileQueryHandler(IAppDbContext dbContext, IDateTime dateTime, IUserRepository userRepository)
    {
      this.dbContext = dbContext;
      this.dateTime = dateTime;
      this.userRepository = userRepository;
    }

    public async Task<long> Handle(UpdateUserProfileQuery request, CancellationToken cancellationToken)
    {
      var user = await this.userRepository.GetUserProfileByIdAsync(request.Id);

      if(user is null)
      {
        throw new NotFoundException("User not found.");
      }

      if (user.IsDeleted)
      {
        throw new AlreadyDeletedException("User is deleted.");
      }

      user.CodeName = request.CodeName.Trim();
      user.Bio = request.Bio.Trim();
      user.AboutMe = request.AboutMe.Trim();
      user.FirstName = request.FirstName.Trim();
      user.LastName = request.LastName.Trim();

      user.ModifiedOn = this.dateTime.Now;

      await this.dbContext.SaveChangesAsync(cancellationToken);

      return user.Id;
    }
  }
}
