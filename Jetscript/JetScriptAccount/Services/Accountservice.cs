using JetScriptAccount.Data;
using JetScriptAccount.Models;
using Microsoft.EntityFrameworkCore;

namespace JetScriptAccount.Services
{
    public class AccountService
    {
        private readonly AppDbContext _context;

        public AccountService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AccountInfoDto?> GetAccountInfo(string userName)
        {
            var result = await (
                from u in _context.Users
                join a in _context.Accounts
                    on u.user_id equals a.user_id
                where u.name.ToLower() == userName.ToLower()
                select new AccountInfoDto
                {
                    UserId = u.user_id,
                    UserName = u.name,
                    Email = u.email,
                    Phone = u.phone,
                    AccountId = a.account_id,
                    AccountNumber = a.account_number,
                    AccountType = a.account_type,
                    Balance = a.balance
                }
            ).FirstOrDefaultAsync();

            return result;
        }
    }
}