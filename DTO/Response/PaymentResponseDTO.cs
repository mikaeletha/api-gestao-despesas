using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.Models;
using System.ComponentModel.DataAnnotations;

namespace api_gestao_despesas.DTO.Response
{
    public class PaymentResponseDTO
    {

        [Key]
        public int PaymentId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public Payment MapExpenseFromPaymentResponseDTO(PaymentResponseDTO paymentResponseDTO)
        {
            Payment payment = new Payment();

            payment.Amount = paymentResponseDTO.Amount;
            payment.PaymentId = paymentResponseDTO.PaymentId;

            return payment;
        }
    }
}
