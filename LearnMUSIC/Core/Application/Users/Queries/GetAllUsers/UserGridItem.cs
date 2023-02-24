using AutoMapper;
using LearnMUSIC.Core.Application._Interfaces.Mapping;
using LearnMUSIC.Core.Domain.Entities;

namespace LearnMUSIC.Core.Application.Users.Queries.GetAllUsers
{
  public class UserGridItem : IHaveCustomMapping
  {
    public long Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public void CreateMappings(Profile configuration)
    {
      configuration.CreateMap<User, UserGridItem>();
    }
  }
}
