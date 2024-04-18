using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.Models;
using System.ComponentModel.DataAnnotations;

namespace api_gestao_despesas.DTO.Response
{
    public class PaymentResponseDTO
    {
        [Key]
        public int Id { get; set; }

        //[Required]
        //public decimal Amount { get; set; }

        [Required]
        public bool PaymentStatus { get; set; }

        public static PaymentResponseDTO Of(Payment payment)
        {
            return new PaymentResponseDTO
            {
                Id = payment.Id,
                PaymentStatus = payment.PaymentStatus
            };
        }
    }
}
