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

        public List<GroupsResponseDTO> Groups { get; set; }

        public List<FriendResponseDTO> Friends { get; set; }

        public static UserResponseDTO Of(User user)
        {
            var groupsModel = new List<Group>();
            foreach (var groupUser in user.GroupUsers)
            {
                groupsModel.Add(groupUser.Groups);
            }


            var groups = new List<GroupsResponseDTO>();
            foreach (Group group in groupsModel)
            {
                groups.Add(GroupsResponseDTO.Of(group));
            }



            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Groups = groups
            };
        }
    }
}
