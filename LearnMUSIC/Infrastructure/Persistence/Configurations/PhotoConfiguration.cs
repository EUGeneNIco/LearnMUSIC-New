using LearnMUSIC.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnMUSIC.Infrastructure.Persistence.Configurations
{
  public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
  {
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
      builder.Property(p => p.Url)
        .HasMaxLength(200);

      builder.Property(p => p.PublicId)
        .HasMaxLength(200);

      builder.Property(b => b.UserId)
                .IsRequired();

      builder.HasOne(p => p.User)
        .WithMany()
        .OnDelete(DeleteBehavior.NoAction);
    }
  }
}
