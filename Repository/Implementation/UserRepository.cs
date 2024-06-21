using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.Models;
using api_gestao_despesas.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
            var users = await _context.Users
                .Include(u => u.Groups)
                .ToListAsync();
            return users;
        }

        public async Task<User> GetById(int id)
        {
            var user = await _context.Users
                .Include(u => u.Groups)
                .FirstOrDefaultAsync(c => c.Id == id);
            return user;
        }

        public async Task<User> GetByEmail( string email)
        {
            var user = await _context.Users
                .Include(u => u.Groups)
                .FirstOrDefaultAsync(c => c.Email == email);
            return user;
        }

        public async Task<User> Create(User user)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existingUser != null)
            {
                throw new InvalidOperationException("Email already registered.");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); // Hash the password before saving

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Update(int id, User user)
        {
            var findUser = await _context.Users.FindAsync(id);

            if (findUser == null)
            {
                return null; 
            }
            findUser.Name = user.Name;
            findUser.Email = user.Email;
            findUser.PhoneNumber = user.PhoneNumber;

            await _context.SaveChangesAsync();

            return findUser;
        }

        public async Task<User> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAllByIds(List<int> ids)
        {
            var user = await _context.Users
                .Include(u => u.Groups)
                .Where(t => ids.Contains(t.Id))
                .ToListAsync();
            return user;
        }

        public async Task<string> Login(User user)
        {
            var userDB = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (userDB == null || BCrypt.Net.BCrypt.Verify(user.Password, userDB.Password))
            {
                throw new UnauthorizedAccessException("Usuário ou senha incorretos.");
            }

            return GenerateJwtToken(userDB);
        }

        private string GenerateJwtToken(User model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Ry74cBQva5dThwbwchR9jhbtRFnJxWSZ");
            var claims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, model.Email.ToString()),
                new Claim(ClaimTypes.NameIdentifier, model.Id.ToString())
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<User> UpdatePassword(int id, User user)
        {
            var findUser = await _context.Users.FindAsync(id);

            if (findUser == null)
            {
                return null; // Retorna null se o usuário não for encontrado
            }

            // Atualiza a senha do usuário
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await _context.SaveChangesAsync();

            return user;
        }
    }
}
