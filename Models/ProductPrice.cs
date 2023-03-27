using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Websitara2.Models
{
	public class ProductPrice
	{
        [Key]
        public int PriceId { get; set; }
        public Double Price { get; set; }
        public string? Date { get; set; }
        public virtual Product? Product { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; } 
    }
}