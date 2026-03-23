using System;
using System.Collections.Generic;
using System.Text;

namespace JetScriptAccount.Models
{
    public class AccountUpdateRequest
    {
        public string AccountType { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }
}
