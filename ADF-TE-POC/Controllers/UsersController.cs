using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ADF_TE_POC.Configurations;
using ADF_TE_POC.DTOs;
using ADF_TE_POC.Models;
using ADF_TE_POC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace ADF_TE_POC.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtSettings _jwtSettings;

        public UsersController(IUserService userService, IOptions<JwtSettings> jwtSettings)
        {
            _userService = userService;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost]
        [Route("token")]
        [AllowAnonymous]
        public async Task<IActionResult> Token(TokenRequest request)
        {
            var validUser = await _userService.Validate(request.Email, request.Password);
            if (validUser==null)
                return Unauthorized("Invalid email or password.");
            var token = GenerateJwt(validUser);
            return Ok(new
            {
                access_token = token,
                IssuedAt = _jwtSettings.IssuedAtUtc.Value,
                expires = _jwtSettings.ExpiresUtc.Value,
                email = request.Email
            });
        }

        private string GenerateJwt(User user)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
            };
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtSettings.Audience,
                EncryptingCredentials = _jwtSettings.EncryptingCredentials.Value,
                Expires = _jwtSettings.ExpiresUtc.Value,
                IssuedAt = _jwtSettings.IssuedAtUtc.Value,
                Issuer = _jwtSettings.Issuer,
                NotBefore = _jwtSettings.NotBeforeUtc.Value,
                SigningCredentials = _jwtSettings.SigningCredentials.Value,
                Subject = new ClaimsIdentity(claims)
            };
            var jwtSecurityToken = jwtSecurityTokenHandler.CreateJwtSecurityToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
        }
    }
}
