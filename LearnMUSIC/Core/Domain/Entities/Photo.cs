using LearnMUSIC.Core.Domain.Entities.Base;

namespace LearnMUSIC.Core.Domain.Entities
{
  public class Photo : EntityBase
  {
    public string Url { get; set; }

    public bool IsMain { get; set; }

    public string PublicId { get; set; }

    public long UserId { get; set; }

    public virtual User User { get; set; }
  }
}
