using api_gestao_despesas.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_gestao_despesas.DTO.Request
{
    public class PaymentRequestDTO

    {
        [Required]
        public decimal ValuePayment { get; set; }

        [Required]
        public bool PaymentStatus { get; set; }

        [Required]
        public int expenseId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
