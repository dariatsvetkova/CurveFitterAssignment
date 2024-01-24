using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurveFitter.Server
{
    public class User
    {
        public int Id { get; set; }

        public List<Archive> Archives { get; set; }
    }

    public class Archive
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public required string FitType { get; set; }
        public required double[] Equation { get; set; }
        public required DataPoint[] UserDataPoints { get; set; }
        public required DataPoint[] FitDataPoints { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]

        public User User { get; set; }
    }

    public class ArchiveContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Archive> Archives { get; set; }

        public string DbPath { get; }

        public ArchiveContext()
        {
            DbPath = "archive.db";
        }

        // Configure connection to the database file on disk
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Create tables
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Archive>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Archive>()
                .HasOne(a => a.User)
                .WithMany(u => u.Archives)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Archive>()
                .Property(a => a.FitType)
                .IsRequired();

            modelBuilder.Entity<Archive>()
                .Property(a => a.Equation)
                .IsRequired();

            modelBuilder.Entity<Archive>()
                .Property(a => a.UserDataPoints)
                .IsRequired();

            modelBuilder.Entity<Archive>()
                .Property(a => a.FitDataPoints)
                .IsRequired();
        }
    }
}
