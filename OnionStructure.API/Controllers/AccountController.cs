using OnionStructure.API.ViewModels.Request;
using OnionStructure.Service.Services.Abstract;
using OnionStructure.Contract.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace OnionStructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;
        public AccountController(ILogger<AccountController> logger,
                                 IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(AccountLoginViewModel model)
        {
            _logger.LogInformation("Login was called!!!");
            var loginDto = new AccountLoginDto
            {
                UserName = model.UserName,
                Password = model.Password
            }; //This will be converted to AutoMapper in future.
            var result = await _accountService.Login(loginDto);

            return Ok(result);
        }

        [HttpGet("IsAuthenticated")]
        public bool IsAuthenticated()
        {
            return User.Identity.IsAuthenticated;
        }
    }
}
