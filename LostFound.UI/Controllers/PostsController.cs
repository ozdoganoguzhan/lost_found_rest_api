using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LostFound.Core.Entities;
using LostFound.Infrastructure;
using Microsoft.OpenApi.Writers;

namespace LostFound.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly LFDbContext _context;

        public PostsController(LFDbContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            if (_context.Posts == null)
            {
                return NotFound();
            }
            return await _context.Posts.ToListAsync();
        }
        [HttpGet("last-post-id")]
        public async Task<ActionResult<int>> GetLastPost()
        {
            
            if (_context.Posts == null)
            {
                return NotFound();
            }
            var posts = await _context.Posts.ToListAsync();
            var lastId = await _context.SaveChangesAsync();

            if (posts == null)
            {
                return BadRequest();
            }
            else
            {
                return lastId;
            }

        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
          if (_context.Posts == null)
          {
              return NotFound();
          }
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostSearch(int userId = 0, string categoryId = "none")
        {
            if (_context.Posts == null || userId == 0 && categoryId == "none")
            {
                return BadRequest();
            }

            else
            {
                List<Post> temp = new List<Post>();
                var posts = await _context.Posts.ToListAsync();

                if (userId > 0 && categoryId != "none")
                {

                    foreach (var post in posts)
                    {
                        if (post.UserId != userId || post.CategoryId != categoryId) continue;
                        temp.Add(post);
                    }

                }
                else if (userId > 0)
                {
                    foreach (var post in posts)
                    {
                        if (post.UserId != userId) continue;
                        temp.Add(post);
                    }

                }
                else if (categoryId != "none")
                {

                    foreach (var post in posts)
                    {
                        if (post.CategoryId != categoryId) continue;
                        temp.Add(post);
                    }

                }
                else
                {
                    return NoContent();
                }

                if (temp.Count == 0) return NotFound();

                else return temp;

            }

        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.PostId)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
          if (_context.Posts == null)
          {
              return Problem("Entity set 'LFDbContext.Posts'  is null.");
          }
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.PostId }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (_context.Posts == null)
            {
                return NotFound();
            }
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostExists(int id)
        {
            return (_context.Posts?.Any(e => e.PostId == id)).GetValueOrDefault();
        }
    }
}
