using api_gestao_despesas.Models;
using System.ComponentModel.DataAnnotations;

namespace api_gestao_despesas.DTO.Request
{
    public class ExpenseRequestDTO
    {
        [Required]
        public decimal ValueExpense { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }

        public int GroupId { get; set; }
    }
}
