using api_gestao_despesas.Models;
using System.ComponentModel.DataAnnotations;

namespace api_gestao_despesas.DTO.Request
{
    public class FriendRequestDTO
    {

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int UserId { get; set; }

    }
}

