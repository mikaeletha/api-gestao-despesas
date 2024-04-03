using Microsoft.EntityFrameworkCore;
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

        public DbSet<Group> Groups { get; set; }

        public DbSet<Friend> Friends { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
