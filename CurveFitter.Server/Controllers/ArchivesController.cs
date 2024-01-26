using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Xml;

namespace CurveFitter.Server.Controllers
{
    [Route("api/archives")]
    [ApiController]
    public class ArchivesController() : ControllerBase
    //public class ArchivesController(DataContext context) : ControllerBase
    {
        //private readonly DataContext _context = context;

        //private bool ArchiveExists(int id)
        //{
        //    return _context.archives.any(a => a.id == id);
        //}

        //private bool UserExists(int id)
        //{
        //    return id == 1;
        //}

        // GET: api/Archives/
        [HttpGet]
        public async Task<ActionResult<string>> GetArchives()
        {
            return "Hello World";
        }

        //// GET: api/Archives/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<string>> GetArchive(int id)
        //{
        //    return id.ToString();
        //    //Task<ActionResult<IEnumerable<Archive>>>
        //    //return await _context.Archives.Where(a => a.UserId == id).ToListAsync();
        //}

        //// POST: api/Archives
        //[HttpPost]
        //public async Task<ActionResult<Archive>> PostArchive(Archive archive)
        //{
        //    if (UserExists(archive.UserId) == false)
        //    {
        //        return NotFound("User not found");
        //    }

        //    //double[] values = { 1.2, 3.4, 5.6 };
        //    //byte[] data = new byte[values.Length * sizeof(double)];
        //    //Buffer.BlockCopy(values, 0, data, 0, data.Length);

        //    //MyEntity entity = new MyEntity
        //    //{
        //    //    Name = "Entity 1",
        //    //    Values = data
        //    //};

        //    //using (var context = new MyDbContext())
        //    //{
        //    //    context.MyEntities.Add(entity);
        //    //    context.SaveChanges();
        //    //}

        //    _context.Archives.Add(archive);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetArchive", new { id = archive.Id }, archive);
        //}

        //// DELETE: api/Archives/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Archive>> DeleteArchive(int id)
        //{
        //    if (ArchiveExists(id) == false)
        //    {
        //        return NotFound();
        //    }
        //    var archive = await _context.Archives.FindAsync(id);

        //    _context.Archives.Remove(archive);
        //    await _context.SaveChangesAsync();

        //    return archive;
        //}
    }
}
