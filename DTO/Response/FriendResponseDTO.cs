using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.Models;
using System.ComponentModel.DataAnnotations;

namespace api_gestao_despesas.DTO.Response
{
    public class FriendResponseDTO
    {
        [Required]
        public int FriendId { get; set; }

        [Required]
        public int UserId { get; set; }

        public static FriendResponseDTO Of(Friend friend)
        {
            return new FriendResponseDTO
            {
                FriendId = friend.FriendId,
                UserId = friend.UserId
            };
        }


        public static FriendResponseDTO Of(User user, User friend)
        {
            return new FriendResponseDTO
            {
                FriendId = friend.Id,
                UserId = user.Id
            };
        }

    }
}