﻿using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.DTO.Response;
using api_gestao_despesas.Models;
using Microsoft.AspNetCore.Connections;

namespace api_gestao_despesas.Repository.Interface
{
    public interface IGroupsRepository
    {
        Task<List<Group>> GetAll();
        Task<Group> GetById(int IdGroup);
        Task<Group> Create(Group groups);
        Task<Group> Update(Group groups);
        Task<Group> Delete(int IdGroup);
    }
}