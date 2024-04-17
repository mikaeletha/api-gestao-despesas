using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.DTO.Response;
using api_gestao_despesas.Models;
using Microsoft.AspNetCore.Connections;

namespace api_gestao_despesas.Repository.Interface
{
    public interface IFriendRepository
    {
        Task<List<Friend>> GetAll();
        Task<Friend> GetById(int id);
        Task<Friend> Create(Friend friends);
        Task<Friend> Update(int id, Friend friends);
        Task<Friend> Delete(int id);
    }
}