using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using api_gestao_despesas.Models;

namespace api_gestao_despesas.Controllers
{
    public class PaymentController : Controller
    {
        
        //codigo classe payment
        private readonly List<Payment> _payments = new List<Payment>(); // Simulação de um repositório de pagamentos

        [HttpPost("payment")]
        public ActionResult<Payment> CreatePayment([FromBody] Payment payment)
        {
            _payments.Add(payment);
            return CreatedAtAction(nameof(GetPaymentById), new { paymentId = payment.PaymentId }, payment);
        }

        [HttpGet("payment/{paymentId}")]
        public ActionResult<Payment> GetPaymentById(int paymentId)
        {
            var payment = _payments.FirstOrDefault(p => p.PaymentId == paymentId);
            if (payment == null)
                return NotFound();
            return payment;
        }

        [HttpGet("payment")]
        public ActionResult<IEnumerable<Payment>> GetAllPayments()
        {
            return _payments;
        }

        [HttpPut("payment/{paymentId}")]
        public IActionResult UpdatePayment(int paymentId, [FromBody] Payment updatedPayment)
        {
            var payment = _payments.FirstOrDefault(p => p.PaymentId == paymentId);
            if (payment == null)
                return NotFound();

            // Atualizar os campos do pagamento
            // Exemplo: payment.Amount = updatedPayment.Amount;

            return NoContent();
        }

        [HttpDelete("payment/{paymentId}")]
        public IActionResult DeletePayment(int paymentId)
        {
            var payment = _payments.FirstOrDefault(p => p.PaymentId == paymentId);
            if (payment == null)
                return NotFound();

            _payments.Remove(payment);
            return NoContent();
        }
    }
}
