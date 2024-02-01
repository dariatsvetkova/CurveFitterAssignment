using Microsoft.AspNetCore.Mvc;
using CurveFitter.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CurveFitter.Server.Controllers
{
    [ApiController]
    public class ArchivesController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;
        private readonly DbUtils _dbUtils = new DbUtils(context);

        // GET: api/archives?user=5
        [Route("api/archives")]
        [HttpGet("{user}")]
        public async Task<ActionResult<IEnumerable<Archive>>> GetArchive(int user)
        {
            if (!_dbUtils.UserExists(user))
            {
                return NotFound("User not found");
            }
            return await _context.Archives.Where(a => a.UserId == user).ToListAsync();
        }

        // POST: api/archives/add
        [Route("api/archives/add")]
        [HttpPost]
        public async Task<ActionResult<ArchiveToSave>> PostArchive([FromBody]ArchiveToSave newArchiveObj)
        {
            // Validate inputs

            (bool isValid, string errorMessage) = ServerUtils.ValidateArchive(newArchiveObj);

            if (!isValid)
            {
                return BadRequest(errorMessage);
            }

            if (_dbUtils.UserExists(newArchiveObj.UserId) == false)
            {
                return NotFound("User not found");
            }

            // Save archive to the database

            int uniqueId = ServerUtils.GenerateId();
            while (_dbUtils.ArchiveExists(uniqueId))
            {
                uniqueId = ServerUtils.GenerateId();
            }

            Archive newArchive = new Archive
            {
                Id = uniqueId,
                Timestamp = DateTime.Now,
                FitType = newArchiveObj.FitType,
                Name = newArchiveObj.Name,
                UserId = newArchiveObj.UserId,
                UserDataPoints = newArchiveObj.UserDataPoints,
                FitDataPoints = newArchiveObj.FitDataPoints,
                Equation = newArchiveObj.Equation
            };

            try
            {
                _context.Archives.Add(newArchive);
                await _context.SaveChangesAsync();

                return new JsonResult(newArchive);
            }
            catch (Exception Ex)
            {
                return BadRequest(new { message = Ex.InnerException?.Message ?? "Unhandled exception" });
            }
        }

        // DELETE: api/archives/delete?id=5
        [Route("api/archives/delete")]
        [HttpDelete]
        public async Task<ActionResult<int>> DeleteArchive(int id)
        {
            // TBD: add auth and make sure user owns archive

            if (_dbUtils.ArchiveExists(id) == false)
            {
                return NotFound();
            }

            try
            {
                var archive = await _context.Archives.FindAsync(id);

                _context.Archives.Remove(archive);
                await _context.SaveChangesAsync();

                return id;
            }
            catch (Exception Ex)
            {
                return BadRequest(new { message = Ex.InnerException?.Message ?? "Unhandled exception" });
            }
        }
    }
}
