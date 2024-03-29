using api_gestao_despesas.Models;
using System.ComponentModel.DataAnnotations;

namespace api_gestao_despesas.DTO.Request
{
    public class GroupsRequestDTO

    {
        [Required]
        public int Id_friend { get; set; }

        [Required]
        public string  NameGroup { get; set; }

    }
}
