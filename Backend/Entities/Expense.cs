using System;
using System.ComponentModel.DataAnnotations;

namespace Projekt.Entities
{
    public class Expense
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string? UserId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
