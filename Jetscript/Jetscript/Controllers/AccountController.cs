using JetScriptAccount.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jetscript.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> GetAccount(string userName)
        {
            var account = await _accountService.GetAccountInfo(userName);

            if (account == null)
            {
                return NotFound("Account not found");
            }

            return Ok(account);
        }
    }
} 