using AutoMapper;
using LearnMUSIC.Core.Application._Interfaces.Mapping;
using LearnMUSIC.Core.Domain.Entities;

namespace LearnMUSIC.Core.Application.Users.Queries.SearchForBandmates
{
  public class SearchForBandmatesDto : IHaveCustomMapping
  {
    public long Id { get; set; }

    public string FullName { get; set; }

    public string CodeName { get; set; }

    public string AboutMe { get; set; }

    public string PhotoUrl { get; set; }

    public void CreateMappings(Profile configuration)
    {
      configuration.CreateMap<User, SearchForBandmatesDto>()
        .ForMember(x => x.FullName, s => s.MapFrom(b => $"{b.FirstName} {b.LastName}"))
        .ForMember(b => b.PhotoUrl, s => s.MapFrom(x => !x.Photos.Any() ? "" : x.Photos.Select(x => x.Url).First()));
    }
  }
}
