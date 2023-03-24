using AutoMapper;
using LearnMUSIC.Core.Application._Interfaces.Mapping;
using LearnMUSIC.Core.Application.Photos.Models;
using LearnMUSIC.Core.Application.UserInstruments.Models;
using LearnMUSIC.Core.Domain.Entities;
using System.Collections.Generic;
using System.Security.Claims;

namespace LearnMUSIC.Core.Application.Users.Models
{
  public class UserProfileDto : IHaveCustomMapping
  {
    public string UserName { get; set; }

    public string CodeName { get; set; }

    public string Bio { get; set; }

    public string AboutMe { get; set; }

    public string PasswordHash { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhotoUrl { get; set; }

    public string Token { get; set; }

    public virtual ICollection<UserInstrumentDto> Instruments { get; set; }

    public void CreateMappings(Profile configuration)
    {
      configuration.CreateMap<User, UserProfileDto>()
        .ForMember(b => b.PhotoUrl, s => s.MapFrom(x => !x.Photos.Any() ? "" : x.Photos.Select(x => x.Url).First()));
    }
  }
}
