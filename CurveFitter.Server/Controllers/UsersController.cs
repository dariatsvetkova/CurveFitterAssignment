using CurveFitter.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core;

namespace CurveFitter.Server.Controllers
{
    [ApiController]
    public class UsersController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        private bool UserExists(int id)
        {
            return _context.Users.Any(a => a.Id == id);
        }

        private int GetUniqueId()
        {
            int id = ServerUtils.GenerateId();
            while (UserExists(id))
            {
                id = ServerUtils.GenerateId();
            }
            return id;
        }

        // GET: users/exists/5
        [HttpGet]
        [Route("api/users/exists/{id}")]
        public ActionResult<bool> Exists(int id)
        {
            return UserExists(id);
        }

        // POST: Users/Create
        [HttpPost]
        [Route("api/users/create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<string>> CreateUserAsync()
        {
            return "Hello World2";
            //try
            //{
            //    int newUserId = GetUniqueId();

            //    User newUser = new User
            //    {
            //        Id = newUserId,
            //        Archives = []
            //    };

            //    _context.Users.Add(newUser);
            //    await _context.SaveChangesAsync();

            //    return CreatedAtAction("CreateUserAsync", new { id = newUser.Id }, newUser);
            //}
            //catch (DbUpdateException Ex)
            //{
            //    return BadRequest(Ex.InnerException?.Message ?? "DbUpdateException");
            //}
            //catch (EntityCommandExecutionException Ex)
            //{
            //    return BadRequest(Ex.InnerException?.Message ?? "EntityCommandExecutionException");
            //}
            //catch (InvalidOperationException Ex)
            //{
            //    return BadRequest(Ex.InnerException?.Message ?? "InvalidOperationException");
            //}
            //catch (Exception Ex)
            //{
            //    return BadRequest(Ex.InnerException?.Message ?? "Unknown Exception");
            //}
        }
    }
}
