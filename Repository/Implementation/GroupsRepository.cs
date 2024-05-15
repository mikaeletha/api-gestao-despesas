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
                .Include(g => g.Owner)
                .Include(g => g.Friends)
                .ToListAsync();
            
            return groups;
        }

        public async Task<Group> GetById(int id)
        {
            // Carregar o grupo com despesas, pagamentos e usuários
            var group = await _context.Groups
                .Include(g => g.Expenses)
                    .ThenInclude(e => e.Payments)
                .Include(g => g.Owner)
                .Include(g => g.Friends)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (group == null)
            {
                throw new InvalidOperationException("Group not found.");
            }

            // Calcular as despesas para cada usuário do grupo
            await CalculateExpensesForEachUser(id);

            return group;
        }

        public async Task<Group> Update(int id, Group groups)
        {
            await CalculateExpensesForEachUser(id);
            var findGroup = await _context.Groups.FindAsync(id);
            if (findGroup == null)
            {
                throw new InvalidOperationException("Grupo não pode ser atualizado pois não foi encontrado");
            }
            findGroup.NameGroup = groups.NameGroup;

            await _context.SaveChangesAsync();
            return groups;
        }

        public async Task<Group> AddGroupUsers(int id, int ownerId)
        {
            var existingGroup = await _context.Groups.FirstOrDefaultAsync(c => c.Id == id);
            var existingUsers = await _context.Users.FirstOrDefaultAsync(u => ownerId == id);

            if (existingGroup == null || existingUsers == null)
            {
                throw new InvalidOperationException();
            }

            if (existingGroup.Friends.Contains(existingUsers))
            {
                throw new InvalidOperationException();
            }

            existingGroup.Friends.Add(existingUsers);
            _context.Groups.Update(existingGroup);
            await _context.SaveChangesAsync();

            return existingGroup;
        }
        public async Task<Group> AddGroupFriendsUser(int groupId, int ownerId, int userId)
        {
            var existingGroup = await _context.Groups.Include(g => g.Friends).FirstOrDefaultAsync(g => g.Id == groupId);
            var existingUser = await _context.Users.Include(u => u.Groups).FirstOrDefaultAsync(u => u.Id == ownerId);
            var existingFriend = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (existingGroup == null || existingUser == null || existingFriend == null)
            {
                throw new InvalidOperationException("Group, user, or friend not found.");
            }

            if (existingGroup.Friends.Any(u => u.Id == userId))
            {
                throw new InvalidOperationException("Friend is already in the group.");
            }

            existingGroup.Friends.Remove(existingGroup.Owner);
            existingGroup.Friends.Add(existingFriend);
            await _context.SaveChangesAsync();

            return existingGroup;
        }


        public async Task<Group> DeleteGroupFriendsUser(int groupId, int ownerId, int userId)
        {
            var existingGroup = await _context.Groups.Include(g => g.Friends).FirstOrDefaultAsync(g => g.Id == groupId);
            var existingUser = await _context.Users.Include(u => u.Groups).FirstOrDefaultAsync(u => u.Id == ownerId);
            var existingFriend = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (existingGroup == null || existingUser == null || existingFriend == null)
            {
                throw new InvalidOperationException("Group, user, or friend not found.");
            }

            var friendToRemove = existingGroup.Friends.FirstOrDefault(u => u.Id == userId);
            if (friendToRemove == null)
            {
                throw new InvalidOperationException("Friend is not in the group.");
            }
            existingGroup.Friends.Remove(friendToRemove);
            await _context.SaveChangesAsync();

            return existingGroup;
        }

        public async Task<Group> CalculateExpensesForEachUser(int groupId)
        {
            var group = await _context.Groups
                .Include(g => g.Expenses)
                    .ThenInclude(e => e.Payments)
                .Include(g => g.Friends)
                .FirstOrDefaultAsync(g => g.Id == groupId);

            if (group == null)
            {
                throw new InvalidOperationException("Group not found.");
            }

            decimal totalExpenses = 0;

            // Calcular o total de despesas
            foreach (var expense in group.Expenses)
            {
                totalExpenses += expense.ValueExpense;
            }

            // Calcular o número total de usuários, incluindo o proprietário
            var totalUsers = group.Friends.Count + 1;

            // Dividir o valor total das despesas pelo número total de usuários
            var expenseSharePerUser = totalExpenses / totalUsers;

            // Atribuir o valor total das despesas ao campo TotalExpense do grupo
            group.TotalExpense = totalExpenses;
            group.ExpenseShare = expenseSharePerUser;

            // Resetar os valores de paymentMade e amountToPay para todos os usuários
            foreach (var user in group.Friends)
            {
                user.PaymentMade = false;
                user.AmountToPay = expenseSharePerUser;
            }

            // Resetar os valores de paymentMade e amountToPay para o proprietário
            var owner = group.Owner;
            owner.PaymentMade = false;
            owner.AmountToPay = expenseSharePerUser;

            // Marcar o pagamento como feito para o usuário que realizou o pagamento
            foreach (var expense in group.Expenses)
            {
                foreach (var payment in expense.Payments)
                {
                    if (payment.PaymentStatus)
                    {
                        var friend = group.Friends.FirstOrDefault(u => u.Id == payment.UserId);
                        var ownerGroup = group.Owner;
                        if (friend != null)
                        {
                            var amountPay = friend.AmountToPay - payment.ValuePayment;
                            if (amountPay > 0)
                            {
                                friend.PaymentMade = false;
                            }
                            else
                            {
                                friend.PaymentMade = true;
                            }
                            friend.AmountToPay = amountPay;
                        }
                        if (ownerGroup.Id == payment.UserId)
                        {
                            var amountPay = ownerGroup.AmountToPay - payment.ValuePayment;
                            if(amountPay > 0)
                            {
                                ownerGroup.PaymentMade = false;
                            } else
                            {
                                ownerGroup.PaymentMade = true;
                            }
                            ownerGroup.AmountToPay = amountPay;
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();
            return group;
        }
    }
}
