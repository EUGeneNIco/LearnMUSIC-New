using AFPMBAI.CLAIMS.WebAPI.Auth;
using AutoMapper;
using LearnMUSIC.Controllers;
using LearnMUSIC.Core.Application._Exceptions;
using LearnMUSIC.Core.Application.Users.Command.CreateUser;
using LearnMUSIC.Core.Application.Users.Models;
using LearnMUSIC.Core.Domain.Contracts;
using LearnMUSIC.Interface.WebAPI.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnMUSIC.Interface.WebAPI.Controllers
{
  public class AuthController : ApiControllerBase
  {
    private readonly IJwtAuthenticationManager authManager;
    private readonly IUserRepository repository;
    private readonly IMapper mapper;

    public AuthController(IJwtAuthenticationManager authManager, IUserRepository repository, IMapper mapper)
    {
      this.authManager = authManager;
      this.repository = repository;
      this.mapper = mapper;
    }

    //Register
    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> RegisterUser([FromBody] CreateUserCommand command)
    {
      try
      {
        var data = await this.Mediator.Send(command);

        return new JsonResult(data);
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

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<UserProfileDto>> Login([FromBody] UserCredentials credentials)
    {
      try
      {
        var token = await this.authManager.Authenticate(credentials.Username, credentials.Password);

        var user = await this.repository.GetUserByUsernameAsync(credentials.Username);

        if (token is null)
        {
          return Unauthorized();
        }

        var userDto = this.mapper.Map<UserProfileDto>(user);
        userDto.Token = token;

        return Ok(userDto);
      }
      catch (UnauthorizedAccessException ex)
      {
        if (ex.Message.Contains("Unauthorized"))
        {
          return Unauthorized(ex.Message);
        }
        else if (ex.Message.Contains("Forbidden"))
        {
          //return Forbid(ex.Message);
          return Unauthorized(ex.Message);
        }
        else
        {
          return Unauthorized(ex.Message);
        }
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }
  }
}
