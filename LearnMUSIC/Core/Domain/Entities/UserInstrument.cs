using LearnMUSIC.Core.Domain.Entities.Base;

namespace LearnMUSIC.Core.Domain.Entities
{
  public class UserInstrument : EntityBase
  {
    public long UserId { get; set; }

    public virtual User User { get; set; }

    public long InstrumentId { get; set; }

    public virtual CodeListValue Instrument { get; set; }
  }
}
