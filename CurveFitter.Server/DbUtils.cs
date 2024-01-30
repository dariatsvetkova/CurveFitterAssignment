using CurveFitter.Server.Models;

namespace CurveFitter.Server
{
    public class DbUtils(DataContext context)
    {
        private readonly DataContext _context = context;

        public bool UserExists(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }

        public bool ArchiveExists(int id)
        {
            return _context.Archives.Any(a => a.Id == id);
        }
    }
}
