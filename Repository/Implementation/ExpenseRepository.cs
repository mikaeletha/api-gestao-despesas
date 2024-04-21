using api_gestao_despesas.Models;
using api_gestao_despesas.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
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
            await _context.SaveChangesAsync();
            return expense;
        }

        public async Task<List<Expense>> GetAll()
        {

            var expenses = await _context.Expenses.Include(e => e.Payments)
                .ToListAsync();
            return expenses;
        }

        public async Task<Expense> GetById(int id)
        {
            var expense =  await _context.Expenses.Include(e => e.Payments)
                .FirstOrDefaultAsync(e => e.Id == id);
            return expense;
        }

        public async Task<Expense> Update(int id, Expense expense)
        {
            var findExpense = await _context.Expenses.FindAsync(id);
            
            if (findExpense == null)
            {
                throw new InvalidOperationException("Despesa não foi encontrada");
            }
            findExpense.Description = expense.Description;
            findExpense.ValueExpense = expense.ValueExpense;
            findExpense.Date = expense.Date;
            await _context.SaveChangesAsync();
            return expense;
        }
    }
}
