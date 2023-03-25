
using LearnMusic.Core.Domain.Enumerations;
using LearnMUSIC.Common.Helper;
using LearnMUSIC.Core.Application._Interfaces;
using LearnMUSIC.Core.Domain.Entities;
using LearnMUSIC.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFPMBAI.CLAIMS.DbUpdate.DbScripts
{
  internal class DbScript_0_0_2_0
  {
    private static IAppDbContext _dbContext = null;

    private static List<User> users;

    internal static void Run(IAppDbContext dbContext, CodeListValue lastDbVersionEntity)
    {
      var thisVersion = new Version("0.0.2.0");
      var lastDbVersion = new Version(lastDbVersionEntity.Name);

      if (lastDbVersion < thisVersion)
      {
        _dbContext = dbContext;

        users = new List<User>
        {
          new User
          {
            UserName = "lebron",
            Email = "lebron_thegot@gmail.com",
            PasswordHash = PasswordHelper.Hash("12345"),
            FirstName = "Lebron James",
            LastName = "Yap",
            AccountStatus = true,
            AboutMe = "I am a college student from USA. Thank ya!",
            Bio = "If you dont fear anyone, thats because you dont know me.",
            CodeName = "TheGot",
            CreatedOn = DateTime.Now
          },

          new User
          {
            UserName = "chishiya",
            Email = "chishiya123@gmail.com",
            PasswordHash = PasswordHelper.Hash("12345"),
            FirstName = "Chishiya",
            LastName = "Tokomoto",
            AccountStatus = true,
            AboutMe = "Arigatou gozaimasu!! Im a good boy from Japan.",
            Bio = "nana korobi ya oki (Fall seven times, get up eight.)",
            CodeName = "goodBoyFromJapan",
            CreatedOn = DateTime.Now
          },

          new User
          {
            UserName = "togo",
            Email = "togo@gmail.com",
            PasswordHash = PasswordHelper.Hash("12345"),
            FirstName = "Togo",
            LastName = "Chooks",
            AccountStatus = true,
            AboutMe = "Please try you call later.",
            Bio = "When I hear good music, I feel immortality.",
            CodeName = "yourRockerDog",
            CreatedOn = DateTime.Now
          }
        };

        try
        {
          SeedUsers();

          SeedInstrumentToUsers();

          SeedNonAdminAccess();

          lastDbVersionEntity.Name = thisVersion.ToString();

          _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
          Console.Write($"\n\nSeeding Error: {ex.InnerException.Message}");
        }
      }
    }

    private static void SeedUsers()
    {
      foreach (var user in users)
      {
        if (!_dbContext.Users.Any(x => x.UserName == user.UserName))
        {
          _dbContext.Users.Add(user);
        }
      }

      _dbContext.SaveChanges();
    }

    private static void SeedInstrumentToUsers()
    {
      var dbUsers = _dbContext.Users.ToList();
      var dbInstruments = _dbContext.CodeListValues
        .Where(x => x.Type == CodeListType.Instrument)
        .ToList();

      foreach (var user in dbUsers)
      {
        var index = dbUsers.IndexOf(user);

        if (user.UserName != "eugene")
        {
          _dbContext.UserInstruments.Add(new UserInstrument
          {
            User = user,
            Instrument = dbInstruments[index],
            CreatedOn = DateTime.Now,
          });
        }
        else 
        {
          foreach (var instrument in dbInstruments.Where(x => x.Name == "Acoustic Guitar" || x.Name == "Electric Guitar" || x.Name == "Bass Guitar"))
          {
            _dbContext.UserInstruments.Add(new UserInstrument
            {
              User = user,
              Instrument = instrument,
              CreatedOn = DateTime.Now,
            });
          }
        }
      }
    }

    private static void SeedNonAdminAccess()
    {
      var users = _dbContext.Users
          .Include(p => p.ModuleAccesses)
              .ThenInclude(p => p.Module)
          .Where
          (x => x.UserName != "eugene")
          .ToList();


      var modules = _dbContext.Modules.Where(x => x.Category == "Usual").ToList();

      modules.ForEach(module =>
      {
        foreach (var user in users)
        {
          if (!user.ModuleAccesses.Any(p => p.Module != null && p.Module.Name == module.Name))
          {
            user.ModuleAccesses.Add(new UserModuleAccess
            {
              ModuleId = module.Id,
              HasAccess = true
            });
          }
        }
      });

      _dbContext.SaveChanges();
    }
  }
}
