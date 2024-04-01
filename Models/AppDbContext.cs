using api_gestao_despesas.Models.ModelConfiguration;
using Microsoft.EntityFrameworkCore;
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
                .WithMany(e => e.Payments)
                .HasForeignKey(p => p.ExpenseId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Group> Groups { get; set; }
    }
}
