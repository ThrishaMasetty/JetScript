using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JetScriptAccount.Models
{
    [Table("transactions")]
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int transaction_id { get; set; }

        public int account_id { get; set; }

        public string transaction_type { get; set; } = string.Empty;

        public decimal amount { get; set; }

        public DateTime transaction_date { get; set; }
    }
}