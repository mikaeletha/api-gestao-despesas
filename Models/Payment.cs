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
        public decimal Amount { get; set; }
        // Outros campos do pagamento

        [Required]
        public int ExpensesId { get; set; }
        public Expense Expense { get; set; }
    }
}