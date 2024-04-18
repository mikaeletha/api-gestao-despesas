using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.Models;
using System.ComponentModel.DataAnnotations;

namespace api_gestao_despesas.DTO.Response
{
    public class GroupsResponseWithOutUsersDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NameGroup { get; set; }

        [Required]
        public List<ExpenseResponseDTO> Expenses { get; set; }


        public static GroupsResponseWithOutUsersDTO Of(Group group)
        {
            var expenses = new List<ExpenseResponseDTO>();
            if (group.Expenses != null)
            {
                foreach (Expense expense in group.Expenses)
                {
                    expenses.Add(ExpenseResponseDTO.Of(expense));
                }
            }

            return new GroupsResponseWithOutUsersDTO
            {
                Id = group.Id,
                NameGroup = group.NameGroup,
                Expenses = expenses
            };
        }

    }
}
