using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LostFound.Core.Entities;
using LostFound.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.AspNetCore.JsonPatch;

namespace LostFound.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly LFDbContext _context;

        public UsersController(LFDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;


        }
        // PATCH: api/v2/Employees/5
        [HttpPatch("{id}")]
        public async Task<ActionResult<User>> PatchUser(int id, [FromBody] JsonPatchDocument<User> userDocument)
        {
            if (userDocument == null)
            {
                return BadRequest(ModelState);
            }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            userDocument.ApplyTo(user, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Update(user);
            await _context.SaveChangesAsync();

            return new ObjectResult(user);
        }

        // GET: api/Users/5
        [HttpGet("search")]
        public async Task<ActionResult<User>> GetUserSearch(string email="")
        {
            if (_context.Users == null || email == "")
            {
                return NotFound();
            }

            else
            {
                var user = _context.Users.FirstOrDefault(x => x.Email == email);
                if (user == null)
                {
                    return NotFound();
                }

                return user;
            }

        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'LFDbContext.Users'  is null.");
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
