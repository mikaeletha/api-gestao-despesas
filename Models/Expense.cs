using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_gestao_despesas.Models
{
    [Table("Expense")]
    public class Expense
    {
        [Key]
        public int ExpensesId { get; set; }

        [Required]
        public decimal ValueExpense { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int GroupId { get; set; }

        [Required]
        public ICollection<Payment> Payments { get; set; }
    }
}
