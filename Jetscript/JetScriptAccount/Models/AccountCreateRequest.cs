using System;
using System.Collections.Generic;
using System.Text;

namespace JetScriptAccount.Models
{
    public class AccountCreateRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }
}
