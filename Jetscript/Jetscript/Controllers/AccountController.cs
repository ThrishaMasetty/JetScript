using JetScriptAccount.Models;
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

        // GET : api/account/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            var account = await _accountService.GetAccountById(id);

            if (account == null)
            {
                return NotFound("Account not found");
            }

            return Ok(account);
        }

        // POST : api/account
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountCreateRequest request)
        {
            var result = await _accountService.CreateAccount(request);

            if (result == null)
            {
                return BadRequest("User not found.");
            }

            return Ok(new
            {
                message = "Account created successfully",
                data = result
            });
        }

        // PUT : api/account/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] AccountUpdateRequest request)
        {
            var result = await _accountService.UpdateAccount(id, request);

            if (result == null)
            {
                return NotFound("Account not found");
            }

            return Ok(new
            {
                message = "Account updated successfully",
                data = result
            });
        }

        // DELETE : api/account/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var deleted = await _accountService.DeleteAccount(id);

            if (!deleted)
            {
                return NotFound("Account not found");
            }

            return Ok("Account deleted successfully");
        }
    }
}