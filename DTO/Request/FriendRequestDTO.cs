using api_gestao_despesas.Models;
using System.ComponentModel.DataAnnotations;

namespace api_gestao_despesas.DTO.Request
{
    public class FriendRequestDTO
    {      
       
        public int? Id { get; set; }

        [Required]
        public string CPF { get; set; }

    }
}

