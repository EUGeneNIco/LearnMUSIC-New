using AutoMapper;
using LearnMUSIC.Application.SongSheets.Commands.CreateSongSheet;
using LearnMUSIC.Application.SongSheets.Queries.GetSongSheetById;
using LearnMUSIC.Application.SongSheets.Commands.UpdateSongSheet;
using LearnMUSIC.Application.SongSheets.Models;
using LearnMUSIC.Application.SongSheets.Queries.GetSongSheetById;
using LearnMUSIC.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearnMUSIC.Application.SongSheets.Queries.GetSongSheetById;
using LearnMUSIC.Controllers;
using LearnMUSIC.Core.Application._Exceptions;
using Microsoft.AspNetCore.Authorization;
using LearnMUSIC.Core.Application.SongSheets.Queries.GetAllSongSheets;
using LearnMUSIC.Core.Application.SongSheets.Commands.DeleteSongSheet;

namespace LearnMUSIC.Interface.WebAPI.Controllers
{
  [AllowAnonymous]
  public class SongSheetsController : ApiControllerBase
  {
    //Create
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create([FromBody] CreateSongSheetCommand command)
    {
      try
      {;
        //command.UserId = this.GetUserIdFromClaims();

        var data = await this.Mediator.Send(command);

        return new JsonResult(data);
      }
      catch(NotFoundException ex)
      {
        return BadRequest(ex.Message);
      }
      catch (DuplicateException ex)
      {
        return BadRequest(ex.Message);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }
    ////Delete
    [HttpPost("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete([FromRoute]long id)
    {
      try
      {
        var data = await this.Mediator.Send(new DeleteSongSheetCommand { Id = id });

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
    //Update
    [HttpPost("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Update([FromBody] UpdateSongSheetCommand command)
    {
      try
      {
        var data = await this.Mediator.Send(command);

        return new JsonResult(data);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }
    //Get All
    [HttpGet("getAll/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetAll(long id)
    {
      try
      {
        var data = await this.Mediator.Send(new GetAllSongSheetsQuery { UserId = id });

        return new JsonResult(data);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetById([FromRoute]long id)
    {
      try
      {
        var data = await this.Mediator.Send(new GetSongSheetByIdQuery { Id = id });

        return new JsonResult(data);
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
