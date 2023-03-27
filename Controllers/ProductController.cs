using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Websitara2.Models;

namespace Websitara2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ContextApiDbContext _context;

        public ProductController(ContextApiDbContext context)
        {
            _context = context;
            if (!_context.Products.Any())
            {

                var product = new List<Product>
                {

                    new Product { ProductId = 001, Product_name = "Dell", CategoryId =001},
                    new Product { ProductId = 002, Product_name = "Macbook Air", CategoryId =001},
                    new Product { ProductId = 003, Product_name = "Lenovo", CategoryId =001},
                    new Product { ProductId = 004, Product_name = "samsung s22 ultra", CategoryId =002},
                    new Product { ProductId = 005, Product_name = "Iphone 14 ", CategoryId =002},
                    new Product { ProductId = 006, Product_name = "Vivo v32", CategoryId =002},
                    new Product { ProductId = 007, Product_name = "Samsung Max", CategoryId =003},
                    new Product { ProductId = 008, Product_name = "Apple Ipad 12", CategoryId =003},
                    new Product { ProductId = 009, Product_name = "Samsung Tab3", CategoryId =003}

                 };
                _context.Products.AddRange(product);
                _context.SaveChangesAsync();

            }
        }

      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
            return await _context.Products.Include(pp=> pp.ProductPrice).ToListAsync();
        }

       
        [HttpGet]
        [Route("GetProduct/{ProductId}")]
        public async Task<ActionResult<Product>> GetProduct(int ProductId)
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
            var product = await _context.Products
                .Include(pp => pp.ProductPrice)
                .SingleOrDefaultAsync(w => w.ProductId == ProductId);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

       
        [HttpPut("{ProductId}")]
        public async Task<IActionResult> PutProduct(int ProductId, Product product)
        {
            if (ProductId != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

       
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
          if (_context.Products == null)
          {
              return Problem("Entity set 'ContextApiDbContext.Products'  is null.");
          }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { ProductId = product.ProductId }, product);
        }

       
        [HttpDelete("{ProductId}")]
        public async Task<IActionResult> DeleteProduct(int ProductId)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(ProductId);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int ProductId)
        {
            return (_context.Products?.Any(e => e.ProductId == ProductId)).GetValueOrDefault();
        }
    }
}
