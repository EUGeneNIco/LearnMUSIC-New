using LearnMUSIC.Controllers;
using LearnMUSIC.Core.Application._Exceptions;
using LearnMUSIC.Core.Application.Feedbacks.Commands.AddFeedback;
using LearnMUSIC.Core.Application.Feedbacks.Queries.GetAllFeedbacks;
using LearnMUSIC.Core.Application.Users.Queries.GetAllUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnMUSIC.Interface.WebAPI.Controllers
{
  [AllowAnonymous]
  public class AdminController : ApiControllerBase
  {
    [HttpGet("getAllUsers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetAllUsers()
    {
      try
      {
        var data = await this.Mediator.Send(new GetAllUsersQuery { });

        return new JsonResult(data);
      }
      //catch (NotFoundException ex)
      //{
      //  return BadRequest(ex.Message);
      //}
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }

    [HttpGet("getAllFeedBack")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetAllFeedback()
    {
      try
      {
        var data = await this.Mediator.Send(new GetAllFeedbacksQuery { });

        return new JsonResult(data);
      }
      //catch (NotFoundException ex)
      //{
      //  return BadRequest(ex.Message);
      //}
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }
  }
}
