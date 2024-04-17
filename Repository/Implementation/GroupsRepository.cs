using api_gestao_despesas.Models;
using api_gestao_despesas.Repository.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<Group> Delete(int id)
        {
            var groups = await _context.Groups.FindAsync(id);
            _context.Groups.Remove(groups);
            await _context.SaveChangesAsync(); 
            return groups;

            
        }

        public async Task<List<Group>> GetAll()
        {
            var groups = await _context.Groups
                .Include(g => g.Expenses)
                    .ThenInclude(e => e.Payments)
                .Include(g => g.GroupUsers)
                    .ThenInclude(g => g.Users)
                .ToListAsync();

            return groups;
        }

        public async Task<Group> GetById(int id)
        {
            var group = await _context.Groups
                .Include(g => g.Expenses)
                    .ThenInclude(e => e.Payments)
                .Include(g => g.GroupUsers)
                    .ThenInclude(g => g.Users)
                .FirstOrDefaultAsync(g => g.Id == id);

            return group;
        }

        public async Task<Group> Update(int id, Group groups)
        {
            var findGroup = await _context.Groups.FindAsync(id);
            _context.Groups.Update(groups);
            await _context.SaveChangesAsync();
            return groups;
        }

        public async Task<GroupUsers> AddGroupUsers(int id, GroupUsers groupsUsers)
        {
            var existingGroupUsers = await _context.Groups.FirstOrDefaultAsync(c => c.Id == id);
            var existingUsers = await _context.Users.FirstOrDefaultAsync(u => groupsUsers.UserId == id);
            if ( existingGroupUsers != null && existingUsers != null)
            {
                _context.GroupUsers.Add(groupsUsers);
                await _context.SaveChangesAsync();
            }

            return groupsUsers;
        }
    }
}
