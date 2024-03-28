using api_gestao_despesas.Models;
using api_gestao_despesas.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace api_gestao_despesas.Repository.Implementation
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _context;

        public ExpenseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Expense> Create(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            return expense;
        }

        public async Task<Expense> Delete(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            _context.Expenses.Remove(expense);
            return expense;
        }

        public async Task<List<Expense>> GetAll()
        {
            var expense = await _context.Expenses.ToListAsync();
            return expense;
        }

        public async Task<Expense> GetById(int id)
        {
            return await _context.Expenses.FindAsync(id);
        }

        public async Task<Expense> Update(int id, Expense expense)
        {
            var findExpense = await _context.Expenses.FindAsync(id);
            _context.Update(expense);
            await _context.SaveChangesAsync();
            return expense;
        }
    }
}
