

using LearnMUSIC.Core.Application._Exceptions;
using LearnMUSIC.Core.Application.Users.Models;
using LearnMUSIC.Core.Application.Users.Queries.AuthenticateUser;
using LearnMUSIC.Infrastructure;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace LearnMUSIC.Interface.WebAPI.Auth
{
  public class JwtAuthenticationManager : IJwtAuthenticationManager
  {
    private readonly string _key;
    private readonly IMediator _mediator;

    /******************************************************************************/

    public JwtAuthenticationManager(string key, IMediator mediator)
    {
      _key = key;
      _mediator = mediator;
    }

    /******************************************************************************/

    public async Task<string> Authenticate(string userName, string passWord)
    {
      try
      {
        var result = await _mediator.Send(new AuthenticateUserQuery { Username = userName, Password = passWord });

        var token = this.GenerateToken(result);

        return token;
      }
      catch (UnauthorizedException ex)
      {
        throw new UnauthorizedAccessException(ex.Message);
      }
      catch (ForbiddenException ex)
      {
        throw new UnauthorizedAccessException(ex.Message);
      }
    }

    public async Task<bool> ValidateTokenAsync(string token)
    {
      return await Task.FromResult(this.ValidateToken(token));
    }

    /******************************************************************************/

    private string GenerateToken(UserClaimsDto userClaims)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var tokenKey = Encoding.ASCII.GetBytes(_key);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[] {
                    new Claim("UserGuid", userClaims.UserGuid.ToString()),
                    new Claim(ClaimTypes.Name, userClaims.DisplayName),
                    new Claim(ClaimTypes.Email, userClaims.EmailAddress)
                }),
        Expires = new MachineDateTime().Now.AddMinutes(480), // 8 Hours
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
      };

      // Additional Claims
      foreach (var claim in userClaims.Claims)
      {
        tokenDescriptor.Subject.AddClaim(claim);
      }

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }

    private TokenValidationParameters GetValidationParameters()
    {
      return new TokenValidationParameters()
      {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)), // The same key as the one that generate the token
        ValidateAudience = false, // Because there is no audiance in the generated token
        ValidateIssuer = false, // Because there is no issuer in the generated token
        ValidateLifetime = true, // Expiration
        RequireExpirationTime = true
        //ValidIssuer = "Sample",
        //ValidAudience = "Sample"
      };
    }

    private bool ValidateToken(string authToken)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var validationParameters = GetValidationParameters();

      SecurityToken validatedToken;
      IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);

      return true;
    }
  }
}
