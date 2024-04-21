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
        public int UserId { get; set; }

        [Required]
        public int FriendId { get; set; }
    }
}