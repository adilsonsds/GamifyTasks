using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Api.Security;
using Domain.DTO;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers
{
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly TokenConfigurations _tokenConfigurations;

        public AuthController(IUsuarioService usuarioService, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            _usuarioService = usuarioService;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginDTO loginDTO)
        {
            bool credentialsIsValid = false;
            try
            {
                var usuarioDTO = _usuarioService.Autenticar(loginDTO);
                credentialsIsValid = usuarioDTO != null;

                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(usuarioDTO.Email, "Email"),
                    new[]{
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, usuarioDTO.Email)
                    }
                );

                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate.AddSeconds(_tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createDate, expirationDate, handler);

                return Ok(new
                {
                    autenticated = true,
                    created = createDate,
                    expiration = expirationDate,
                    token = token,
                    usuario = usuarioDTO
                });
            }
            catch
            {
                return NotFound();
            }
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            string token = handler.WriteToken(securityToken);

            return token;
        }

        [HttpPost("logout")]
        public ActionResult Logout()
        {
            return NoContent();
        }

    }
}
