using CurveFitter.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace CurveFitter.Server.Controllers
{
    [ApiController]
    public class UsersController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;
        private readonly DbUtils _dbUtils = new DbUtils(context);

        private int GetUniqueId()
        {
            int id = ServerUtils.GenerateId();
            while (_dbUtils.UserExists(id) || id == 0)
            {
                id = ServerUtils.GenerateId();
            }
            return id;
        }

        // POST: api/users/create
        [Route("api/users/create")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult<List<User>>> CreateUserAsync()
        {
            // TBD: add an auth provider and create a user with email/password; validate inputs
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

                return new JsonResult(newUser);
            }
            catch (Exception Ex)
            {
                return BadRequest(new { message = Ex.InnerException?.Message ?? "Unhandled exception" });
            }
        }
    }
}
