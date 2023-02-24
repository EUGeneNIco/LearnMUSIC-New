using LearnMUSIC.Controllers;
using LearnMUSIC.Core.Application._Exceptions;
using LearnMUSIC.Core.Application.Feedbacks.Commands.AddFeedback;
using LearnMUSIC.Core.Application.Feedbacks.Queries.GetAllFeedbacks;
using LearnMUSIC.Core.Application.Users.Command.UpdateUserProfile;
using LearnMUSIC.Core.Application.Users.Queries.GetAllUsers;
using LearnMUSIC.Core.Application.Users.Queries.GetUserProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnMUSIC.Interface.WebAPI.Controllers
{
  [AllowAnonymous]
  public class UserController : ApiControllerBase
  {
    [HttpGet("getUserProfile/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetProfile([FromRoute] GetUserProfileQuery query)
    {
      try
      {
        var data = await this.Mediator.Send(query);

        return new JsonResult(data);
      }
      catch (NotFoundException ex)
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

    [HttpPost("updateUserProfile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetProfile([FromBody] UpdateUserProfileQuery query)
    {
      try
      {
        var data = await this.Mediator.Send(query);

        return new JsonResult(data);
      }
      catch (NotFoundException ex)
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
