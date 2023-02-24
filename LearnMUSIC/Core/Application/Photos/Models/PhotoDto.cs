using AutoMapper;
using LearnMUSIC.Core.Application._Interfaces.Mapping;
using LearnMUSIC.Core.Domain.Entities;

namespace LearnMUSIC.Core.Application.Photos.Models
{
  public class PhotoDto : IHaveCustomMapping
  {
    public string Url { get; set; }

    public bool IsMain { get; set; }

    public string PublicId { get; set; }

    public long UserId { get; set; }

    public void CreateMappings(Profile configuration)
    {
      configuration.CreateMap<Photo, PhotoDto>();
    }
  }
}
