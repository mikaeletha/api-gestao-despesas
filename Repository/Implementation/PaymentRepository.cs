using api_gestao_despesas.Models;
using api_gestao_despesas.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace api_gestao_despesas.Repository.Implementation
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> Create(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<Payment> Delete(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<List<Payment>> GetAll()
        {
            var payment = await _context.Payments.ToListAsync();
            return payment;
        }

        public async Task<Payment> GetById(int id)
        {
            return await _context.Payments.FindAsync(id);
        }

        public async Task<Payment> Update(int id, Payment payment)
        {
            var findPayment = await _context.Payments.FindAsync(id);

            findPayment.Amount = payment.Amount;
            findPayment.ExpenseId = payment.ExpenseId; 

            await _context.SaveChangesAsync();
            return payment;
        }
    }
}
