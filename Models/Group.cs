using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_gestao_despesas.Models
{
    [Table("Groups")]
    public class Group 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NameGroup { get; set; }

        public List<Expense> Expenses { get; set; }

        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalExpense { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ExpenseShare { get; set; }

        public User Owner { get; set; }

        public int OwnerId { get; set; }

        public ICollection<User> Friends { get; set; }
    }
}
