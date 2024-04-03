using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.DTO.Response;
using api_gestao_despesas.Models;
using Microsoft.AspNetCore.Connections;

namespace api_gestao_despesas.Repository.Interface
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetAll();
        Task<Payment> GetById(int id);
        Task<Payment> Create(Payment payment);
        Task<Payment> Update(int id, Payment payment);
        Task<Payment> Delete(int id);
    }
}
