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

        // GET ACCOUNT BY ACCOUNT ID
        public async Task<AccountInfoDto?> GetAccountById(int id)
        {
            var result = await (
                from a in _context.Accounts
                join u in _context.Users
                    on a.user_id equals u.user_id
                where a.account_id == id
                select new AccountInfoDto
                {
                    AccountId = a.account_id,
                    UserId = u.user_id,
                    UserName = u.name,
                    Email = u.email,
                    Phone = u.phone,
                    AccountNumber = a.account_number,
                    AccountType = a.account_type,
                    Balance = a.balance,
                    CreatedAt = a.created_at
                }
            ).FirstOrDefaultAsync();

            return result;
        }

        // CREATE ACCOUNT - NO ID INPUT FROM UI
        public async Task<AccountInfoDto?> CreateAccount(AccountCreateRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.name.ToLower() == request.UserName.ToLower());

            if (user == null)
                return null;

            var account = new Account
            {
                user_id = user.user_id,
                account_number = GenerateAccountNumber(),
                account_type = request.AccountType,
                balance = request.Balance,
                created_at = DateTime.UtcNow
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return new AccountInfoDto
            {
                AccountId = account.account_id,
                UserId = user.user_id,
                UserName = user.name,
                Email = user.email,
                Phone = user.phone,
                AccountNumber = account.account_number,
                AccountType = account.account_type,
                Balance = account.balance,
                CreatedAt = account.created_at
            };
        }

        // UPDATE ACCOUNT BY ACCOUNT ID
        public async Task<AccountInfoDto?> UpdateAccount(int id, AccountUpdateRequest request)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.account_id == id);

            if (account == null)
                return null;

            account.account_type = request.AccountType;
            account.balance = request.Balance;

            await _context.SaveChangesAsync();

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.user_id == account.user_id);

            if (user == null)
                return null;

            return new AccountInfoDto
            {
                AccountId = account.account_id,
                UserId = user.user_id,
                UserName = user.name,
                Email = user.email,
                Phone = user.phone,
                AccountNumber = account.account_number,
                AccountType = account.account_type,
                Balance = account.balance,
                CreatedAt = account.created_at
            };
        }

        public async Task<bool> DeleteAccount(int id)
        {
            await using var dbTransaction = await _context.Database.BeginTransactionAsync();

            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.account_id == id);

            if (account == null)
                return false;

            var transactions = await _context.Transactions
                .Where(t => t.account_id == id)
                .ToListAsync();

            if (transactions.Any())
            {
                _context.Transactions.RemoveRange(transactions);
                await _context.SaveChangesAsync();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            await dbTransaction.CommitAsync();
            return true;
        }

        private string GenerateAccountNumber()
        {
            return "ACC" + DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        }
    }
}