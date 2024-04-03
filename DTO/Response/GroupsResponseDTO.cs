using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.Models;
using System.ComponentModel.DataAnnotations;

namespace api_gestao_despesas.DTO.Response
{
    public class GroupsResponseDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NameGroup { get; set; }

        [Required]
        public ICollection<ExpenseResponseDTO> Expenses { get; set; }

    }
}
