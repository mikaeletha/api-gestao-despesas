using System.ComponentModel.DataAnnotations;

namespace api_gestao_despesas.DTO.Request
{
    public class UpdateUserRequestDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
