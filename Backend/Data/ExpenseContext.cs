using System;
using Microsoft.EntityFrameworkCore;
using Projekt.Entities;

namespace Projekt.Data
{
    public class ExpenseContext : DbContext
    {
        public ExpenseContext(DbContextOptions<ExpenseContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PushSubscriptionModel> PushSubscriptions { get; set; } // Dodajemy tę linię

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure relationships and other settings
        }
    }
}

