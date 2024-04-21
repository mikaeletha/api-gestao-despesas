using api_gestao_despesas.Models;
using System.ComponentModel.DataAnnotations;

namespace api_gestao_despesas.DTO.Request
{
    public class GroupsRequestDTO

    {
        [Required]
        public string  NameGroup { get; set; }

        [Required]
        public int OwnerId { get; set; }

    }
}
