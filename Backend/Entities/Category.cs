﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projekt.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<Expense>? Expenses { get; set; }
    }
}