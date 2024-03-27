﻿using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations.Rules;

namespace api_gestao_despesas.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<Payment> Payments { get; set; }
    }
}