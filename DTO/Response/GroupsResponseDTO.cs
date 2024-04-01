﻿using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.Models;
using System.ComponentModel.DataAnnotations;

namespace api_gestao_despesas.DTO.Response
{
    public class GroupsResponseDTO
    {
        [Key]
        public int IdGroup { get; set; }

        [Required]
        public string NameGroup { get; set; }

    }
}