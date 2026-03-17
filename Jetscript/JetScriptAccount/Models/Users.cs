using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JetScriptAccount.Models
{
    [Table("users")]
    public class Users
    {
        [Key]
        public int user_id { get; set; }

        public string name { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public string phone { get; set; } = string.Empty;

        public DateTime created_at { get; set; }
    }
}