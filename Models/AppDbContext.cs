using Azure;
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

            builder.Entity<Friend>()
                .HasOne(f => f.User)
                .WithMany(f => f.Friends)
                .HasForeignKey(f => f.userId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Group>()
                .HasMany(g => g.Users)
                .WithMany(g => g.Groups)
                .UsingEntity("GroupUsers",
                    l => l.HasOne(typeof(User)).WithMany().HasForeignKey("UserId").HasPrincipalKey(nameof(User.Id)),
                    r => r.HasOne(typeof(Group)).WithMany().HasForeignKey("GroupId").HasPrincipalKey(nameof(Group.Id)),
                    j => j.HasKey("GroupId", "UserId"));

            //builder.Entity<GroupUsers>()
            //    .HasKey(g => new { g.UserId, g.GroupId });

            //builder.Entity<GroupUsers>()
            //    .HasOne(g => g.Users)
            //    .WithMany(g => g.GroupUsers)
            //    .HasForeignKey(g => g.UserId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<GroupUsers>()
            //    .HasOne(g => g.Groups)
            //    .WithMany(g => g.GroupUsers)
            //    .HasForeignKey(g => g.GroupId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<User>()
            //    .HasMany(u => u.Groups)
            //    .WithMany(g => g.Users)
            //    .UsingEntity("GroupUsers",
            //    l => l.HasOne(typeof(Group))
            //    .WithMany()
            //    .HasForeignKey("GroupId").HasPrincipalKey(nameof(Group.Id)),
            //    r => r.HasOne(typeof(User))
            //    .WithMany()
            //    .HasForeignKey("UserId").HasPrincipalKey(nameof(User.Id)),
            //    j => j.HasKey("UserId", "GroupId"));

            //builder.Entity<User>()
            //     .HasMany(u => u.Groups)
            //     .WithMany(g => g.Users)
            //     .UsingEntity(j => j.ToTable("UserGroups"));


        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GroupUsers> GroupUsers { get; set; }
    }
}
