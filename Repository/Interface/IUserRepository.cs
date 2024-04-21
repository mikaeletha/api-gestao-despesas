using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.DTO.Response;
using api_gestao_despesas.Models;

namespace api_gestao_despesas.Repository.Interface
{
    public interface IUserRepository
    {
        Task<string> Login(User user);
        Task<List<User>> GetAll();
        Task<List<User>> GetAllByIds(List<int> ids);
        Task<User> GetById(int id);
        Task<User> Create(User user);
        Task<User> Update(int id, User user);
        Task<User> Delete(int id);

        Task<User> GetByEmail(string email);
        Task<User> UpdatePassword(int id, User user);
    }
}
