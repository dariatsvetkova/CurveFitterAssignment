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

        // GET: api/archives?userId=5
        [Route("api/archives")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Archive>>> GetArchive(int userId)
        {
            if (!_dbUtils.UserExists(userId))
            {
                return NotFound("User not found");
            }
            return await _context.Archives.Where(a => a.UserId == userId).ToListAsync();
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

        // DELETE: api/archives/delete?userId=5&archiveId=7
        [Route("api/archives/delete")]
        [HttpDelete]
        public async Task<ActionResult<int>> DeleteArchive(int userId, int archiveId)
        {
            if (
                _dbUtils.ArchiveExists(archiveId) == false ||
                _dbUtils.UserExists(userId) == false
            )
            {
                return NotFound();
            }

            try
            {
                Archive archive = await _context.Archives.FindAsync(archiveId);

                // TBD: add auth and make sure the user is authorized to delete the archive

                if (archive.UserId != userId)
                {
                    return Unauthorized();
                }

                _context.Archives.Remove(archive);
                await _context.SaveChangesAsync();

                return archiveId;
            }
            catch (Exception Ex)
            {
                return BadRequest(new { message = Ex.InnerException?.Message ?? "Unhandled exception" });
            }
        }
    }
}
