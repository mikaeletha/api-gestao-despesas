using api_gestao_despesas.Models;
using api_gestao_despesas.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace api_gestao_despesas.Repository.Implementation
{
    public class GroupsRepository : IGroupsRepository
    {
        private readonly AppDbContext _context;

        public GroupsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Group> Create(Group groups)
        {
            _context.Groups.Add(groups);
            await _context.SaveChangesAsync();
            return groups;
        }

        public async Task<Group> Delete(int IdGroup)
        {
            var groups = await _context.Groups.FindAsync(IdGroup);
            _context.Groups.Remove(groups);
            return groups;

            
        }

        public async Task<List<Group>> GetAll()
        {
            var groups = await _context.Groups.ToListAsync();
            return groups;
        }

        public async Task<Group> GetById(int IdGroup)
        {
            return await _context.Groups.FindAsync(IdGroup);
        }

        public async Task<Group> Update(Group groups)
        {
            _context.Update(groups);
            await _context.SaveChangesAsync();
            return groups;
        }
    }
}
