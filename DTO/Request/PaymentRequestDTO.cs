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

        public Payment MapExpenseFromPaymentRequestDTO(PaymentRequestDTO paymentRequestDTO)
        {
            Payment payment= new Payment();

            payment.Amount = paymentRequestDTO.Amount;
            payment.ExpensesId = paymentRequestDTO.ExpensesId;

            return payment;
        }
    }
}
