using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Websitara2.Models;

namespace Websitara2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
        private readonly ContextApiDbContext _context;

        public CategoryController(ContextApiDbContext context)
        {
            _context = context;
            if (!_context.Categories.Any())
            {

                var category = new List<Category>
                {

                    new Category { CategoryId = 001, Category_name = "Laptop"},
                    new Category { CategoryId = 002, Category_name = "Mobile" },
                    new Category { CategoryId = 003, Category_name = "Tablets" }

                 };
                _context.Categories.AddRange(category);
                _context.SaveChangesAsync();

            }
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
          if (_context.Categories == null)
          {
              return NotFound();
          }
            return await _context.Categories
                .Include(p => p.Products)
                
               .ToListAsync();
        }

       
        [HttpGet("{CategoryId}")]
        public async Task<ActionResult<Category>> GetCategory(int CategoryId)
        {
          if (_context.Categories == null)
          {
              return NotFound();
          }
            var category = await _context.Categories
                 .Include(p => p.Products)
                .SingleOrDefaultAsync(w=>w.CategoryId == CategoryId);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

       
  
        [HttpPut("{CategoryId}")]
        public async Task<IActionResult> PutCategory(int CategoryId, Category category)
        {
            if (CategoryId != category.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(CategoryId))
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
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
          if (_context.Categories == null)
          {
              return Problem("Entity set 'ContextApiDbContext.Categories'  is null.");
          }
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { CategoryId = category.CategoryId }, category);
        }

      
        [HttpDelete("{CategoryId}")]
        public async Task<IActionResult> DeleteCategory(int CategoryId)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FindAsync(CategoryId);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int CategoryId)
        {
            return (_context.Categories?.Any(e => e.CategoryId == CategoryId)).GetValueOrDefault();
        }
    }
}
