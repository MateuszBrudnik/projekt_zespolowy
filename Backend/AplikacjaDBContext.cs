/*using System;
using Microsoft.EntityFrameworkCore;
using ProjektST2.Entities;

namespace ProjektST2
{
	public class AplikacjaDBContext : DbContext
    {
        public AplikacjaDBContext(DbContextOptions<AplikacjaDBContext> options) : base(options) { }

        public DbSet<Wydatek> Wydatek { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}

// dotnet ef migrations add "init"
// dotnet ef database update
*/