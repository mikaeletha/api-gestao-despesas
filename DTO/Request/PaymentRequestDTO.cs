using api_gestao_despesas.Models;
using System.ComponentModel.DataAnnotations;

namespace api_gestao_despesas.DTO.Request
{
    public class PaymentRequestDTO

    {

        [Required]
        public decimal Amount { get; set; }
        // Outros campos do pagamento

        [Required]
        public int ExpensesId { get; set; }
    }
}
