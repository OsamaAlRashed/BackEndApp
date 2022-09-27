using BackEndApp.Services.Dtos;
using BackEndApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEndApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginDto dto)
        {
            var result = await accountRepository.Login(dto);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Signup(SignupDto dto)
        {
            var result = await accountRepository.Signup(dto);
            return Ok(result);
        }
    }
}
