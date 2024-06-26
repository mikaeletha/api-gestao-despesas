﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api_gestao_despesas.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [JsonIgnore]
        public string Password { get; set; }

        public bool PaymentMade { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal AmountToPay { get; set; }

        public ICollection<Group> Groups { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
