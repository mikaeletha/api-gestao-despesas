using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_gestao_despesas.Models
{
    [Table("Friends")]
    public class Friend
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int userId { get; set; }

        public User User { get; set; }
    }
}
