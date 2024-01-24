using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace CurveFitter.Server
{
    //public class User
    //{
    //    public int Id { get; set; }

    //    public List<Archive> Archives { get; set; } = [];
    //}

    public class Archive
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public required string FitType { get; set; }
        public required int[] Equation { get; set; }
        public required DataPoint[] UserDataPoints { get; set; }
        public required DataPoint[] FitDataPoints { get; set; }
        public int UserId { get; set; }
        //[ForeignKey("UserId")]

        //public User User { get; set; } = new User();
    }

    public class ArchiveContext: DbContext
    {
        public string DbPath { get; }

        public ArchiveContext()
        {
            DbPath = "Data Source=archive.sqlite";
        }

        public ArchiveContext(DbContextOptions<ArchiveContext> options) : base(options)
        { }

        //public DbSet<User> Users { get; set; }
        public DbSet<Archive> Archives { get; set; }

        private int[] ConvertStringToIntArray(string str)
        {
            return str.Split(',').Select(s => int.Parse(s)).ToArray() ?? new int[0]!;
        }

        // Configure connection to the database file on disk
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
            System.Diagnostics.Debug.WriteLine($"Configured SQLite options with Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            System.Diagnostics.Debug.WriteLine($"Start creating tables...");
            
            builder.Entity<Archive>()
                .HasKey(a => a.Id);

            builder.Entity<DataPoint>()
                .HasKey(d => d.X);

            _ = builder.Entity<Archive>()
                .Property(a => a.Equation)
                .HasConversion(
                    toDb => string.Join(",", toDb),
                    fromDb => ConvertStringToIntArray(fromDb)
                ); 

            base.OnModelCreating(builder);

            // Create tables
            //builder.Entity<User>()
            //    .HasKey(u => u.Id);

            //builder.Entity<Archive>()
            //    .HasOne(a => a.User)
            //    .WithMany(u => u.Archives)
            //    .HasForeignKey(a => a.UserId);

            //builder.Entity<Archive>()
            //    .Property(a => a.FitType)
            //    .IsRequired();

            //builder.Entity<Archive>()
            //    .Property(a => a.Equation)
            //    .IsRequired();

            //builder.Entity<Archive>()
            //    .Property(a => a.UserDataPoints)
            //    .IsRequired();

            //builder.Entity<Archive>()
            //    .Property(a => a.FitDataPoints)
            //    .IsRequired();

            System.Diagnostics.Debug.WriteLine($"Tables created successfully");

        }
    }
}
