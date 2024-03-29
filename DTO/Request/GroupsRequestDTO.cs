using api_gestao_despesas.Models;
using System.ComponentModel.DataAnnotations;

namespace api_gestao_despesas.DTO.Request
{
    public class GroupsRequestDTO

    {
        [Required]
        public int Id_friend { get; set; }

        [Required]
        public string  NameGroup { get; set; }

        public Group MapExpenseFromGroupsRequestDTO(GroupsRequestDTO groupRequestDTO)
        {
            Group groups= new Group();

            groups.Id_friend = paymentRequestDTO.Id_friend;
            groups.NameGroup = paymentRequestDTO.NameGroup;

            return groups;
        }
    }
}
