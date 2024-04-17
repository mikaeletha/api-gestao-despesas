using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.Models;
using System.ComponentModel.DataAnnotations;

namespace api_gestao_despesas.DTO.Response
{
    public class FriendResponseDTO
    {
        [Required]
        public int id { get; set; }
        [Required]
        public int userId { get; set; }

        [Required]
        public int groupId { get; set; }

    }
}