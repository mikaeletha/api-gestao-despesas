using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_gestao_despesas.Models
{
    [Table("Payments")]
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public decimal Amount { get; set; }
        // Outros campos do pagamento

        [Required]
        public int ExpensesId { get; set; }

        [Required]
        public Expense Expense { get; set; }
    }
}