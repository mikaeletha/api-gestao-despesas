using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.Models;
using Azure;
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
        public int GroupId { get; set; }

        [Required]
        public ICollection<PaymentResponseDTO> Payments { get; set; }

        public static ExpenseResponseDTO Of(Expense expense)
        {
            var payments = new List<PaymentResponseDTO>();
            foreach (var payment in expense.Payments)
            {
                payments.Add(PaymentResponseDTO.Of(payment));
            }
            return new ExpenseResponseDTO
            {
                Id = expense.Id,
                ValueExpense = expense.ValueExpense,
                Date = expense.Date,
                Description = expense.Description,
                Payments = payments,
                GroupId = expense.GroupId
            };
        }
    }
}