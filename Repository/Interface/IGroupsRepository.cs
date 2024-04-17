using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.DTO.Response;
using api_gestao_despesas.Models;
using Microsoft.AspNetCore.Connections;

namespace api_gestao_despesas.Repository.Interface
{
    public interface IGroupsRepository
    {
        Task<List<Group>> GetAll();
        Task<Group> GetById(int id);
        Task<Group> Create(Group groups);
        Task<Group> Update(int id, Group groups);
        Task<Group> Delete(int id);
        Task<GroupUsers> AddGroupUsers(int id, GroupUsers groupsUsers);
    }
}
