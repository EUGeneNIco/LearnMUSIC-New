using AutoMapper;
using LearnMUSIC.Core.Application._Exceptions;
using LearnMUSIC.Core.Application._Interfaces;
using LearnMUSIC.Core.Application.Photos.Models;
using LearnMUSIC.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnMUSIC.Core.Application.Photos.Commands.AddPhoto
{
  public class AddPhotoCommandHandler : IRequestHandler<AddPhotoCommand, PhotoDto>
  {
    private readonly IAppDbContext dbContext;
    private readonly IPhotoService photoService;
    private readonly IMapper mapper;

    public AddPhotoCommandHandler(IAppDbContext dbContext, IPhotoService photoService, IMapper mapper)
    {
      this.dbContext = dbContext;
      this.photoService = photoService;
      this.mapper = mapper;
    }

    public async Task<PhotoDto> Handle(AddPhotoCommand request, CancellationToken cancellationToken)
    {
      var user = this.dbContext.Users
        .FirstOrDefault(x => x.Id == request.UserId);

      if (user is null)
      {
        throw new NotFoundException("User not found.");
      }
      if (user.IsDeleted)
      {
        throw new AlreadyDeletedException("User is deleted.");
      }

      var result = await this.photoService.AddPhotoAsync(request.FormFile);

      if (result.Error != null)
      {
        throw new UploadErrorException("Error uploading image.");
      }

      var photo = new Photo
      {
        Url = result.SecureUrl.AbsoluteUri,
        PublicId = result.PublicId,

        CreatedOn = DateTime.Now,
      };

      if (user.Photos.Count == 0)
      {
        photo.IsMain = true;
      }

      user.Photos.Add(photo);

      await this.dbContext.SaveChangesAsync(cancellationToken);

      return this.mapper.Map<PhotoDto>(photo);
    }
  }
}
