using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.DTO.Response;
using api_gestao_despesas.Models;

namespace api_gestao_despesas.Repository.Interface
{
    public interface IExpenseRepository
    {
        Task<List<Expense>> GetAll();
        Task<Expense> GetById(int id);
        Task<Expense> Create(Expense expense);
        Task<Expense> Update(int id, Expense expense);
        Task<Expense> Delete(int id);
        Task Create(Friend createFriend);
    }
}
