using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Websitara2.Models
{
	public class Product
	{
        public Product()
        {
            ProductPrice = new HashSet<ProductPrice>();
        }
        [Key]
        public int ProductId { get; set; }
        public string? Product_name { get; set; }
        public virtual Category? Category { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public virtual ICollection<ProductPrice>? ProductPrice { get; set; }

    }
}

