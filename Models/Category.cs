using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Websitara2.Models
{
	public class Category
	{

        public Category() {
            Products = new HashSet<Product>();
        }
       
        [Key]
        public int CategoryId { get; set; }
        public string? Category_name { get; set; }
        public virtual ICollection<Product>? Products { get; set; }

    }
}



