using api_gestao_despesas.Models;

namespace api_gestao_despesas.DTO.Request
{
    public class GroupUsersRequestDTO
    {
        public int GroupId { get; set; }

        public int UserId { get; set; }
    }
}