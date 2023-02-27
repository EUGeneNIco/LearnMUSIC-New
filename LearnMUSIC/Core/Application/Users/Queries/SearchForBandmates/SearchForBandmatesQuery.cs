using LearnMUSIC.Core.Application.Users.Models;
using MediatR;

namespace LearnMUSIC.Core.Application.Users.Queries.SearchForBandmates
{
  public class SearchForBandmatesQuery : IRequest<IEnumerable<SearchForBandmatesDto>>
  {
    public long UserId { get; set; }
  }
}
