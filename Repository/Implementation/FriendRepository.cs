using api_gestao_despesas.Models;
using api_gestao_despesas.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace api_gestao_despesas.Repository.Implementation
{
    public class FriendRepository : IFriendRepository

    {
        private readonly AppDbContext _context;

        public FriendRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Friend> Create(Friend friends)
        {
            _context.Friends.Add(friends);
            await _context.SaveChangesAsync();
            return friends;
        }

        public async Task<Friend> Delete(int id)
        {
            var friends = await _context.Friends.FindAsync(id);
            _context.Friends.Remove(friends);
            return friends;
        }

        public async Task<List<Friend>> GetAll()
        {
            var friends = await _context.Friends
                .Include(f => f.User)
                .ThenInclude(f => f.GroupUsers)
                .ToListAsync();

            return friends;
        }

        public async Task<Friend> GetById(int id)
        {
            var friendId = _context.Friends
                .Include(f => f.User)
                .ThenInclude(f => f.GroupUsers)
                .FirstOrDefaultAsync();

            return await friendId;
        }

        public async Task<Friend> Update(int id, Friend friends)
        {
            _context.Update(friends);
            await _context.SaveChangesAsync();
            return friends;
        }
    }
}

