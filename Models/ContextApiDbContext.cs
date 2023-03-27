using Microsoft.EntityFrameworkCore;

using System;
using Websitara2.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace Websitara2.Models
{
	public class ContextApiDbContext:DbContext
	{
		public ContextApiDbContext(DbContextOptions options) : base(options)
		{

		}
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPrice> ProductPrices{ get; set; }

        

    }
}

