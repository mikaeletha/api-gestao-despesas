using System.ComponentModel.DataAnnotations;

namespace api_gestao_despesas.DTO.Request
{
    public class UpdateUserPassword
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
