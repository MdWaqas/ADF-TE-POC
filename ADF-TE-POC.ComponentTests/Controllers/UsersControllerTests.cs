using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADF_TE_POC.Configurations;
using ADF_TE_POC.Controllers;
using ADF_TE_POC.DTOs;
using ADF_TE_POC.Models;
using ADF_TE_POC.Services.Impl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shouldly;
using Xunit;

namespace ADF_TE_POC.ComponentTests.Controllers
{
    public class UsersControllerTests : ControllerTestBase
    {
        private readonly UsersController _usersController;
        public UsersControllerTests()
        {
            var jwtSettings = new JwtSettings
            {
                Audience = "http://localhost:44360/",
                ClockSkewSeconds = 5,
                EncryptingSecretKey = "KhR470eRK4jT3gYL",
                Issuer = "http://localhost:44360/",
                SigningSecretKey = "XWXQksSZZGnTj/RwaUvKjVb8m6coeM6Vw6FzCWSomD6uPulVrhBkEPKMtGlT0tCQdGGUVPSo9PAagO1QOzpGoA==",
                ValidForMinutes = 20
            };
            IOptions<JwtSettings> jwtSettingsOptions = Options.Create(jwtSettings);
            _usersController = new UsersController(new UserServices(Context), jwtSettingsOptions);
        }

        [Fact]
        public async Task GET_ShouldReturn_Token()
        {
            var resp = await _usersController.Token(new TokenRequest { Email = "waqas.idrees@confiz.com", Password = "123456" });

            resp.ShouldBeAssignableTo<OkObjectResult>();
        }
    }
}
