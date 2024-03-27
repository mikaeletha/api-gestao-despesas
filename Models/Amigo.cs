using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_gestao_despesas.Models
{
    [Table("Friends")]
    public class Friend
    {
        [Key]
        public int Id_amigo { get; set; }
        [Required]
        public string CPF { get; set; }

    }
}
