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
   
        public List<User> Users { get; set; }
    }
}
