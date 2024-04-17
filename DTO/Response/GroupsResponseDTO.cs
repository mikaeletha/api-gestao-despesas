using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.Models;
using System.ComponentModel.DataAnnotations;

namespace api_gestao_despesas.DTO.Response
{
    public class GroupsResponseDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NameGroup { get; set; }

        [Required]
        public List<ExpenseResponseDTO> Expenses { get; set; }

        [Required]
        public List<UserResponseDTO> Users { get; set; }


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
            

            var usersModel = new List<User>();
            foreach (var groupUser in group.GroupUsers)
            {
                usersModel.Add(groupUser.Users);
            }

            var users = new List<UserResponseDTO>();
            foreach (User user in usersModel)
            {
                users.Add(UserResponseDTO.Of(user));
            }
            return new GroupsResponseDTO
            {
                Id = group.Id,
                NameGroup = group.NameGroup,
                Expenses = expenses,
                Users = users
            };
        }

    }
}
