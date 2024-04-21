using api_gestao_despesas.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace api_gestao_despesas.DTO.Response
{
    public class UserResponseDTO
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

        public bool PaymentMade { get; set; }

        public decimal AmountToPay { get; set; }

        public List<GroupsResponseWithOutUsersDTO> Groups { get; set; }

        public List<FriendResponseDTO> Friends { get; set; }

        public static UserResponseDTO Of(User user, List<User> friends)
        {
            var groups = new List<GroupsResponseWithOutUsersDTO>();
            foreach (Group group in user.Groups)
            {
                groups.Add(GroupsResponseWithOutUsersDTO.Of(group));
            }

            var friendsList = new List<FriendResponseDTO>();
            if (friends != null)
            {
                foreach (var friend in friends)
                {
                    friendsList.Add(FriendResponseDTO.Of(user, friend));
                }
            }
            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                PaymentMade = user.PaymentMade,
                AmountToPay = user.AmountToPay,
                Groups = groups,
                Friends = friendsList
            };
        }
    }
}
