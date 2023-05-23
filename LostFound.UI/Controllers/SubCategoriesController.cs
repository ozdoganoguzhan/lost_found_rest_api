using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LostFound.Core.Entities;
using LostFound.Infrastructure;

namespace LostFound.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : ControllerBase
    {
        private readonly LFDbContext _context;

        public SubCategoriesController(LFDbContext context)
        {
            _context = context;
        }

        // GET: api/SubCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategory>>> GetSubCategories()
        {
            if (_context.SubCategories == null)
            {
                return NotFound();
            }
            return await _context.SubCategories.ToListAsync();
        }

        // GET: api/SubCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategory>> GetSubCategory(string id)
        {
            if (_context.SubCategories == null)
            {
                return NotFound();
            }
            var subCategory = await _context.SubCategories.FindAsync(id);

            if (subCategory == null)
            {
                return NotFound();
            }

            return subCategory;
        }

        // GET: api/Users/5
        [HttpGet("search")]
        public async Task<ActionResult<SubCategory>> GetSubCategoryWithCategoryId(string categoryId = "")
        {
            if (_context.SubCategories == null || categoryId == "")
            {
                return NotFound();
            }

            else
            {
                var subCategory = _context.SubCategories.FirstOrDefault(x => x.categoryId == categoryId);
                if (subCategory == null)
                {
                    return NotFound();
                }

                return subCategory;
            }


        }

        // PUT: api/SubCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubCategory(string id, SubCategory subCategory)
        {
            if (id != subCategory.SubCategoryId)
            {
                return BadRequest();
            }

            _context.Entry(subCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCategoryExists(id))
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

        // POST: api/SubCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubCategory>> PostSubCategory(SubCategory subCategory)
        {
            if (_context.SubCategories == null)
            {
                return Problem("Entity set 'LFDbContext.SubCategories'  is null.");
            }
            _context.SubCategories.Add(subCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubCategory", new { id = subCategory.SubCategoryId }, subCategory);
        }

        // DELETE: api/SubCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory(string id)
        {
            if (_context.SubCategories == null)
            {
                return NotFound();
            }
            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                return NotFound();
            }

            _context.SubCategories.Remove(subCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubCategoryExists(string id)
        {
            return (_context.SubCategories?.Any(e => e.SubCategoryId == id)).GetValueOrDefault();
        }
    }
}
