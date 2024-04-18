using api_gestao_despesas.Models;
using api_gestao_despesas.Repository.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
                .Include(g => g.Users)
                .ToListAsync();

            return groups;
        }

        public async Task<Group> GetById(int id)
        {
            var group = await _context.Groups
                .Include(g => g.Expenses)
                    .ThenInclude(e => e.Payments)
                .Include(g => g.Users)
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

        public async Task<Group> AddGroupUsers(int id, int userId)
        {
            var existingGroup = await _context.Groups.FirstOrDefaultAsync(c => c.Id == id);
            var existingUsers = await _context.Users.FirstOrDefaultAsync(u => userId == id);
            
            if ( existingGroup == null || existingUsers == null)
            {
                throw new InvalidOperationException();
            }
            
            if ( existingGroup.Users.Contains(existingUsers))
            {
                throw new InvalidOperationException();
            }

            existingGroup.Users.Add(existingUsers);
            _context.Groups.Update(existingGroup);
            await _context.SaveChangesAsync();

            return existingGroup;
        }

        //public async Task<Group> AddGroupFriendsUser(int id, int userId, int friendId)
        //{
        //    var existingGroup = await _context.Groups.FirstOrDefaultAsync(c => c.Id == id);
        //    var existingUsers = await _context.Users.FirstOrDefaultAsync(u => userId == id);
        //    var existingFriend = await _context.Users.FirstOrDefaultAsync(f => friendId == id);

        //    if (existingGroup == null || existingUsers == null)
        //    {
        //        throw new InvalidOperationException();
        //    }

        //    if (existingGroup.Users.Contains(existingUsers))
        //    {
        //        throw new InvalidOperationException();
        //    }

        //    if (existingGroup.Users.Contains(existingFriend))
        //    {
        //        throw new InvalidOperationException();
        //    }

        //    existingGroup.Users.Add(existingUsers);
        //    _context.Users.Add(existingFriend);
        //    _context.Groups.Update(existingGroup);
        //    await _context.SaveChangesAsync();

        //    return existingGroup;
        //}
    }
}
