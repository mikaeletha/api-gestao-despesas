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
            await _context.SaveChangesAsync();
            return friends;
        }

        public async Task<List<Friend>> GetAllByUser(int userId)
        {
            
            var friends = await _context.Friends
                .Where(f => f.UserId == userId)
                .ToListAsync();

            return friends;
        }

        public async Task<Friend> GetById(int id)
        {
            var group = await _context.Friends
                .FirstOrDefaultAsync(g => g.Id == id);

            return group;
        }

        public async Task<Friend> Update(int id, Friend friends)
        {
            var findFriend = await _context.Friends.FindAsync(id);
            if (findFriend == null)
            {
                throw new InvalidOperationException();
            }
            _context.Friends.Update(friends);
            await _context.SaveChangesAsync();
            return friends;
        }
    }
}
