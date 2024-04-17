using api_gestao_despesas.Models;
using System.ComponentModel.DataAnnotations;

namespace api_gestao_despesas.DTO.Request
{
    public class FriendRequestDTO
    {
        [Required]
        public int userId { get; set; }
        [Required]
        public int groupId { get; set; }
    }
}

