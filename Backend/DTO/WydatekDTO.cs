using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektST2.DTO
{
	public class WydatekDTO
	{

        [Required]
        [StringLength(15, ErrorMessage = "Wprowadzona nazwa jest za długa")]
        public string Name { get; set; }

        [Required]
        public DateTime ExpenseTime { get; set; }

        [Required]
        public float Price { get; set; }

    }
}

