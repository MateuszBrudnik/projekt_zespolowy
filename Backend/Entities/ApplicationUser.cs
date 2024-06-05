using System;
using Microsoft.AspNetCore.Identity;

namespace Projekt.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}

