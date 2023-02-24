using LearnMUSIC.Controllers;
using LearnMUSIC.Core.Application._Exceptions;
using LearnMUSIC.Core.Application.Feedbacks.Commands.AddFeedback;
using LearnMUSIC.Core.Application.Feedbacks.Queries.GetAllFeedbacks;
using LearnMUSIC.Core.Application.Photos.Commands.AddPhoto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnMUSIC.Interface.WebAPI.Controllers
{
  [AllowAnonymous]
  public class PhotoController : ApiControllerBase
  {
    [HttpPost("addPhoto/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AddPhoto([FromForm] IFormFile file, [FromRoute] long userId)
    {
      try
      {
        var command = new AddPhotoCommand
        {
          UserId = userId,
          FormFile = file,
        };

        var data = await this.Mediator.Send(command);

        return new JsonResult(data);
      }
      catch(NotFoundException ex)
      {
        return BadRequest(ex.Message);
      }
      catch (AlreadyDeletedException ex)
      {
        return BadRequest(ex.Message);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }
  }
}
