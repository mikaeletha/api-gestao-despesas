using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.Models;
using System.ComponentModel.DataAnnotations;

namespace api_gestao_despesas.DTO.Response
{
    public class ExpenseResponseDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal ValueExpense { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public ICollection<PaymentResponseDTO> Payments { get; set; }
    }
}