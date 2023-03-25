using LearnMUSIC.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnMUSIC.Infrastructure.Persistence.Configurations
{
  public class UserConfiguration : IEntityTypeConfiguration<User>
  {
    public void Configure(EntityTypeBuilder<User> builder)
    {
      builder.Property(p => p.Bio)
        .HasMaxLength(200);

      builder.Property(p => p.AboutMe)
        .HasMaxLength(200);

      builder.Property(p => p.CodeName)
        .HasMaxLength(50);

      builder.Property(p => p.UserName)
        .HasMaxLength(50);

      builder.Property(p => p.LastName)
        .HasMaxLength(50);

      builder.Property(p => p.FirstName)
        .HasMaxLength(50);

      builder.Property(p => p.Email)
        .HasMaxLength(50);

      builder.HasMany(p => p.Photos)
        .WithOne(x => x.User)
        .OnDelete(DeleteBehavior.NoAction);

      builder.HasMany(p => p.Instruments)
        .WithOne(x => x.User)
        .OnDelete(DeleteBehavior.NoAction);
    }
  }
}
