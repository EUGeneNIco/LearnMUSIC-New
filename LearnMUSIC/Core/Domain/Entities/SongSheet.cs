using LearnMUSIC.Core.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnMUSIC.Core.Domain.Entities
{
  public class SongSheet : EntityBase
  {
    public string SongTitle { get; set; }

    public string Singer { get; set; }

    public long KeySignatureId { get; set; }

    public virtual CodeListValue KeySignature { get; set; }

    public long GenreId { get; set; }

    public virtual CodeListValue Genre { get; set; }

    public string Contents { get; set; }

    public bool IsDeleted { get; set; }

    public long UserId { get; set; }

    public virtual User User { get; set; }
  }
}
