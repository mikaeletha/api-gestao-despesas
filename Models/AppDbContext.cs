using api_gestao_despesas.DTO.Response;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Validations.Rules;
using System.Reflection.Emit;

namespace api_gestao_despesas.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<Payment>()
                .HasOne(p => p.Expense)
                .WithMany(p => p.Payments)
                .HasForeignKey(p => p.ExpenseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Expense>()
                .HasOne(e => e.Groups)
                .WithMany(e => e.Expenses)
                .HasForeignKey(e => e.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Group>()
                .HasOne(g => g.Owner)
                .WithMany()
                .HasForeignKey(g => g.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Group>()
                .HasMany(g => g.Friends)
                .WithMany(u => u.Groups)
                .UsingEntity(j =>
                {
                    j.ToTable("GroupUsers");
                    j.HasOne(typeof(User)).WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Restrict);
                    j.HasOne(typeof(Group)).WithMany().HasForeignKey("GroupId").OnDelete(DeleteBehavior.Restrict);

                });

            builder.Entity<Payment>()
                .HasOne(p => p.User)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Friend> Friends { get; set; }
    }
}
