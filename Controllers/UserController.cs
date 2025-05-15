using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiUsers.Models;

namespace WebApiUsers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DbUserContext _context;

        public UserController(DbUserContext context)
        {
            _context = context;
        }
       
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users= await _context.Users.ToListAsync();
            return Ok(users);
        }
      
        [HttpGet("GetById")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            
        
            return Ok(user);
        }

        [HttpPost("Post")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            user.FechaCreacion = DateTime.Now;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created, user);
        }

        [HttpPut("Put")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            var userPut = await _context.Users.FindAsync(id);

            if (userPut == null)
            {
                return NotFound();
            }
            userPut.Nombres = user.Nombres;
            userPut.Apellidos = user.Apellidos;
            userPut.Correo = user.Correo;
            userPut.Username = user.Username;

            await _context.SaveChangesAsync();

            return Ok(userPut);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
