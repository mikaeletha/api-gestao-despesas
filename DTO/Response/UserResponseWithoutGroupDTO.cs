using api_gestao_despesas.Models;
using System.ComponentModel.DataAnnotations;

namespace api_gestao_despesas.DTO.Response
{
    public class UserResponseWithoutGroupDTO
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public decimal AmountToPay { get; set; }

        public bool PaymentMade { get; set; }

        public List<FriendResponseDTO> Friends { get; set; }

        public static UserResponseWithoutGroupDTO Of(User user)
        {
            var groups = new List<GroupsResponseWithOutUsersDTO>();
            foreach (Group group in user.Groups)
            {
                groups.Add(GroupsResponseWithOutUsersDTO.Of(group));
            }
            
            return new UserResponseWithoutGroupDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                PaymentMade = user.PaymentMade,
                AmountToPay = user.AmountToPay
            };
        }
    }
}
