using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JetScriptAccount.Models
{
    [Table("accounts")]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int account_id { get; set; }

        public int user_id { get; set; }

        public string account_number { get; set; } = string.Empty;

        public string account_type { get; set; } = string.Empty;

        public decimal balance { get; set; }

        public DateTime created_at { get; set; }
    }
}