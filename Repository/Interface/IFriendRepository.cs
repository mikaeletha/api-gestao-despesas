using api_gestao_despesas.Models;

namespace api_gestao_despesas.Repository.Interface
{
    public interface IFriendRepository
    {
        Task<List<Friend>> GetAll();
        Task<Friend> GetById(int id);
        Task<Friend> Create(Friend friend);
        Task<Friend> Update(int id, Friend friend);
        Task<Friend> Delete(int id);
    }
}
