using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projekt.Entities;

namespace Projekt.Data
{
    public class ExpenseContext : IdentityDbContext<ApplicationUser>
    {
        public ExpenseContext(DbContextOptions<ExpenseContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PushSubscriptionModel> PushSubscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Expense>().HasKey(e => e.Id);
            modelBuilder.Entity<Income>().HasKey(i => i.Id);
            modelBuilder.Entity<PushSubscriptionModel>()
                .HasKey(p => p.Id);
        }
    }
}
