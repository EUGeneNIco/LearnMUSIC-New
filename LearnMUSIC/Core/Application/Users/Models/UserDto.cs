using AutoMapper;
using LearnMUSIC.Core.Application._Interfaces.Mapping;
using LearnMUSIC.Core.Domain.Entities;

namespace LearnMUSIC.Core.Application.Users.Models
{
  public class UserDto : IHaveCustomMapping
  {
    public string UserName { get; set; }

    public string CodeName { get; set; }

    public string Bio { get; set; }

    public string AboutMe { get; set; }

    public string PasswordHash { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public bool AccountStatus { get; set; }

    public DateTime? LastSuccessfulLogin { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsLocked { get; set; }

    //public virtual ICollection<UserModuleAccess> ModuleAccesses { get; set; }

    public void CreateMappings(Profile configuration)
    {
      configuration.CreateMap<User, UserDto>();
    }
  }
}
