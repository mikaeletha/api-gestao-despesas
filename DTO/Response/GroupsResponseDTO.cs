using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_gestao_despesas.DTO.Response
{
    public class GroupsResponseDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NameGroup { get; set; }

        public decimal TotalExpense { get; set; }

        public decimal ExpenseShare { get; set; }

        [Required]
        public List<ExpenseResponseDTO> Expenses { get; set; }

        public UserResponseWithoutGroupDTO Owner { get; set; }

        [Required]
        public List<UserResponseWithoutGroupDTO> Friends { get; set; }


        public static GroupsResponseDTO Of(Group group)
        {
            var expenses = new List<ExpenseResponseDTO>();
            if (group.Expenses != null)
            {
                foreach (Expense expense in group.Expenses)
                {
                    expenses.Add(ExpenseResponseDTO.Of(expense));
                }
            }

            var users = new List<UserResponseWithoutGroupDTO>();
            if (group.Friends != null)
            {
                foreach (User user in group.Friends)
                {
                    users.Add(UserResponseWithoutGroupDTO.Of(user));
                }
            }

            var owner = new UserResponseWithoutGroupDTO();
            if (group.Owner != null)
            {
                owner.Id = group.Owner.Id;
                owner.Name = group.Owner.Name;
                owner.PhoneNumber = group.Owner.PhoneNumber;
                owner.Email = group.Owner.Email;
                owner.PaymentMade = group.Owner.PaymentMade;
                owner.AmountToPay = group.Owner.AmountToPay;
            }

            return new GroupsResponseDTO
            {
                Id = group.Id,
                NameGroup = group.NameGroup,
                TotalExpense = group.TotalExpense,
                ExpenseShare = group.ExpenseShare,
                Expenses = expenses,
                Friends = users,
                Owner = owner
            };
        }

    }
}
