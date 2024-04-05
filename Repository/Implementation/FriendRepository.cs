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

        public async Task<Friend> Delete(int Id)
        {
            var friends = await _context.Friends.FindAsync(Id);
            _context.Friends.Remove(friends);
            return friends;


        }

        public async Task<List<Friend>> GetAll()
        {
            var friends = await _context.Friends.ToListAsync();
            return friends;
        }

        public async Task<Friend> GetById(int Id)
        {
            return await _context.Friends.FindAsync(Id);
        }

        public async Task<Friend> Update(Friend friends)
        {
            _context.Update(friends);
            await _context.SaveChangesAsync();
            return friends;
        }
    }
}

