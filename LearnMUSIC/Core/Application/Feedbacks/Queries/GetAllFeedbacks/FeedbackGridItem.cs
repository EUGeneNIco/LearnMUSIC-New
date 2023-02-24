using AutoMapper;
using LearnMUSIC.Core.Application._Interfaces.Mapping;
using LearnMUSIC.Core.Domain.Entities;

namespace LearnMUSIC.Core.Application.Feedbacks.Queries.GetAllFeedbacks
{
  public class FeedbackGridItem : IHaveCustomMapping
  {
    public long Id { get; set; }

    public string SenderName { get; set; }

    public string Subject { get; set; }

    public void CreateMappings(Profile configuration)
    {
      configuration.CreateMap<Feedback, FeedbackGridItem>()
        .ForMember(dm => dm.SenderName, mo => mo.MapFrom(s => s.User.FirstName));
    }
  }
}
