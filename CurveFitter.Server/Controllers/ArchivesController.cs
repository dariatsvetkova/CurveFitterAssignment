using Microsoft.AspNetCore.Mvc;
using CurveFitter.Server.Models;
using Microsoft.EntityFrameworkCore;

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

        // POST: api/archives/add?user=5&name="My Archive"&fitType=1&userPoints="1y-2,3y-4"&fitPoints="1y-2,3y-4"&equation="-1,-1"
        [Route("api/archives/add")]
        [HttpPost]
        public async Task<ActionResult<Archive>> PostArchive(
            int user,
            string name,
            int fitType,
            string userPoints,
            string fitPoints,
            string equation
        )
        {
            if (_dbUtils.UserExists(user) == false)
            {
                return NotFound("User not found");
            }

            // Convert URL parameters to appropriate types

            DataPoint[] userPointsObj = ServerUtils.StringToDataPoints(userPoints);
            DataPoint[] fitPointsObj = ServerUtils.StringToDataPoints(fitPoints);
            double[] fitEquation = ServerUtils.StringToEquationArr(equation);

            // Validate inputs

            int uniqueId = ServerUtils.GenerateId();
            while (_dbUtils.ArchiveExists(uniqueId))
            {
                uniqueId = ServerUtils.GenerateId();
            }

            Archive newArchive = new Archive
            {
                Id = uniqueId,
                UserId = user,
                Name = name,
                Timestamp = DateTime.Now,
                FitType = fitType,
                UserDataPoints = userPointsObj,
                FitDataPoints = fitPointsObj,
                Equation = fitEquation
            };

            (bool isValid, string errorMessage) = ServerUtils.ValidateArchive(newArchive);
            if (!isValid)
            {
                return BadRequest(errorMessage);
            }

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
