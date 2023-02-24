using CloudinaryDotNet.Actions;

namespace LearnMUSIC.Core.Application._Interfaces
{
  public interface IPhotoService
  {
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

    Task<DeletionResult> DeletePhotoAsync(string publicId);
  }
}
