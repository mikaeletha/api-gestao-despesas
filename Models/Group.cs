using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_gestao_despesas.Models
{
    [Table("Groups")]
    public class Group 
    {
        [Key]
        public int IdGroup { get; set; }
        [Required]
        public int Id_friend { get; set; }
        [Required]
        public string NameGroup { get; set; }

    }
}
