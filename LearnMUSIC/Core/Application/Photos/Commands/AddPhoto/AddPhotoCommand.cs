using LearnMUSIC.Core.Application.Photos.Models;
using MediatR;

namespace LearnMUSIC.Core.Application.Photos.Commands.AddPhoto
{
  public class AddPhotoCommand : IRequest<PhotoDto>
  {
    public long UserId { get; set; }

    public IFormFile FormFile { get; set; }
  }
}
