using AutoMapper;
using LearnMUSIC.Core.Application._Interfaces.Mapping;
using LearnMUSIC.Core.Domain.Entities;

namespace LearnMUSIC.Core.Application.UserInstruments.Models
{
  public class UserInstrumentDto : IHaveCustomMapping
  {
    public string Instrument { get; set; }

    public void CreateMappings(Profile configuration)
    {
      configuration.CreateMap<UserInstrument, UserInstrumentDto>()
        .ForMember(x => x.Instrument, s => s.MapFrom(so => so.Instrument.Name));
    }
  }
}
