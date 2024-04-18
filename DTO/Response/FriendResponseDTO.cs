using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.Models;
using System.ComponentModel.DataAnnotations;

namespace api_gestao_despesas.DTO.Response
{
    public class FriendResponseDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int UserId { get; set; }

        public static FriendResponseDTO Of(Friend friend)
        {
            return new FriendResponseDTO
            {
                Id = friend.Id,
                PhoneNumber = friend.PhoneNumber,
                Email = friend.Email,
                UserId = friend.userId
            };
        }

    }
}