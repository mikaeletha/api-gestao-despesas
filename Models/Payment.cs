using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_gestao_despesas.Models
{
    [Table("Payments")]
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ValuePayment { get; set; }

        [Required]
        public bool PaymentStatus{ get; set; }

        [Required]
        public int ExpenseId { get; set; }
        public Expense Expense { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}