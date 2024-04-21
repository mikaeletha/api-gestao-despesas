using api_gestao_despesas.Models;
using System.ComponentModel.DataAnnotations;

namespace api_gestao_despesas.DTO.Request
{
    public class FriendRequestDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int FriendId { get; set; }
    }
}

