using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektST2.Entities
{
    [Table("Wydatek")]
	public class Wydatek
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string Name { get; set; }

        [Required]
        public DateTime ExpenseTime { get; set; }

        public float Price { get; set; }

    }
}

