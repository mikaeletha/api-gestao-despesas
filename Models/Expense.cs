using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_gestao_despesas.Models
{
    [Table("Expense")]
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName ="decimal(18, 2)")]
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
