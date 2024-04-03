﻿using api_gestao_despesas.Models;
using api_gestao_despesas.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_gestao_despesas.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetById(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(c => c.Id == id);

            // if (user == null) return NotFound();

            return user;
        }

        public async Task<User> Create(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Update(int id, User user)
        {
            var findUser = await _context.Users.FindAsync(id);
            _context.Users.Update(findUser);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Delete(int id)
        {
            var expense = await _context.Users.FindAsync(id);
            _context.Users.Remove(expense);
            await _context.SaveChangesAsync();
            return expense;
        }

    }
}