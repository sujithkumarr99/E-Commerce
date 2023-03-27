using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Websitara2.Models;

namespace Websitara2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly ContextApiDbContext _context;

        public PriceController(ContextApiDbContext context)
        {
            _context = context;
            if (!_context.ProductPrices.Any())
            {

                var productPrice = new List<ProductPrice>
                {

                    new ProductPrice { PriceId = 001, Price = 20000, Date = "2023-03-23",  ProductId = 001},
                    new ProductPrice { PriceId = 002, Price = 40000, Date = "2023-03-23", ProductId = 002},
                    new ProductPrice { PriceId = 003, Price = 30000, Date = "2023-03-23", ProductId = 003},
                    new ProductPrice { PriceId = 004, Price = 80000, Date = "2023-03-23", ProductId = 004},
                    new ProductPrice { PriceId = 005, Price = 90000, Date = "2023-03-23", ProductId = 005},
                    new ProductPrice { PriceId = 006, Price = 20000, Date = "2023-03-23", ProductId = 006},
                    new ProductPrice { PriceId = 007, Price = 50000, Date = "2023-03-23", ProductId = 007},
                    new ProductPrice { PriceId = 008, Price = 40000, Date = "2023-03-23",ProductId = 008},
                    new ProductPrice { PriceId = 009, Price = 25000, Date = "2023-03-23", ProductId = 009}

                 };
                _context.ProductPrices.AddRange(productPrice);
                _context.SaveChangesAsync();

            }
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductPrice>>> GetProductPrices()
        {
          if (_context.ProductPrices == null)
          {
              return NotFound();
          }
            return await _context.ProductPrices.ToListAsync();
        }

        
        [HttpGet("{PriceId}")]
        public async Task<ActionResult<ProductPrice>> GetProductPrice(int PriceId)
        {
          if (_context.ProductPrices == null)
          {
              return NotFound();
          }
            var productPrice = await _context.ProductPrices.FindAsync(PriceId);

            if (productPrice == null)
            {
                return NotFound();
            }

            return productPrice;
        }

       
        [HttpPut("{PriceId}")]
        public async Task<IActionResult> PutProductPrice(int PriceId, ProductPrice productPrice)
        {
            if (PriceId != productPrice.PriceId)
            {
                return BadRequest();
            }

            _context.Entry(productPrice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductPriceExists(PriceId))
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
        public async Task<ActionResult<ProductPrice>> PostProductPrice(ProductPrice productPrice)
        {
          if (_context.ProductPrices == null)
          {
              return Problem("Entity set 'ContextApiDbContext.ProductPrices'  is null.");
          }
            _context.ProductPrices.Add(productPrice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductPrice", new { PriceId = productPrice.PriceId }, productPrice);
        }

       
        [HttpDelete("{PriceId}")]
        public async Task<IActionResult> DeleteProductPrice(int PriceId)
        {
            if (_context.ProductPrices == null)
            {
                return NotFound();
            }
            var productPrice = await _context.ProductPrices.FindAsync(PriceId);
            if (productPrice == null)
            {
                return NotFound();
            }

            _context.ProductPrices.Remove(productPrice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductPriceExists(int PriceId)
        {
            return (_context.ProductPrices?.Any(e => e.PriceId == PriceId)).GetValueOrDefault();
        }
    }
}
