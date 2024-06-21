using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRestRoulette.Context;
using ApiRestRoulette.Models;

namespace ApiRestRoulette.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/{name}
        [HttpGet("{name}")]
        public async Task<ActionResult<User>> GetUser(string name)
        {
            var user = await _context.Users.FindAsync(name);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/name
        [HttpPut("{name}")]
        public async Task<IActionResult> PutUser(string name, User user)
        {
            if (name != user.Name)
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
                if (!UserExists(name))
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

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUser", new { name = user.Name }, user);
            }
            catch (DbUpdateException ex)
            {
                if (UserExists(user.Name))
                {
                    return Conflict("User with the same name already exists.");
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE: api/User/name
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteUser(string name)
        {
            var user = await _context.Users.FindAsync(name);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string name)
        {
            return _context.Users.Any(e => e.Name == name);
        }
    }
}
