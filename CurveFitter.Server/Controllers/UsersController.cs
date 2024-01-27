using CurveFitter.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CurveFitter.Server.Controllers
{
    [ApiController]
    public class UsersController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        private bool UserExists(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }

        private int GetUniqueId()
        {
            return 123456791;
            int id = ServerUtils.GenerateId();
            while (UserExists(id))
            {
                id = ServerUtils.GenerateId();
            }
            return id;
        }

        // GET: api/users
        [Route("api/users")]
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            // return list of all users
            return await _context.Users.ToListAsync();
        }

        // GET: api/users/5
        [Route("api/users/{id}")]
        [HttpGet("{id}")]
        public ActionResult<bool> Exists(int id)
        {
            return UserExists(id);
        }

        // POST: api/users/create
        [Route("api/users/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUserAsync()
        {
            try
            {
                int newUserId = GetUniqueId();

                User newUser = new User
                {
                    Id = newUserId,
                    Archives = []
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return Ok(new { id = newUser.Id });

                //return CreatedAtAction("CreateUserAsync", new { id = 123456791 });
            }
            catch (Exception Ex)
            {
                return BadRequest(new { message = Ex.InnerException?.Message ?? "Unhandled exception" });
            }
        }
    }
}
