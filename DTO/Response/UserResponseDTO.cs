using api_gestao_despesas.Models;
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

        public List<GroupsResponseWithOutUsersDTO> Groups { get; set; }

        public List<FriendResponseDTO> Friends { get; set; }

        public static UserResponseDTO Of(User user)
        {
            var groups = new List<GroupsResponseWithOutUsersDTO>();
            foreach (Group group in user.Groups)
            {
                groups.Add(GroupsResponseWithOutUsersDTO.Of(group));
            }

            var friends = new List<FriendResponseDTO>();
            if (user.Friends != null)
            {
                foreach (var friend in user.Friends)
                {
                    friends.Add(FriendResponseDTO.Of(friend));
                }
            }
            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Groups = groups,
                Friends = friends
            };
        }
    }
}
