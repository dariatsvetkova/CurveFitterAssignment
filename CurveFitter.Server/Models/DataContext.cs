using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace CurveFitter.Server.Models
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext() { }

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Archive> Archives { get; set; }

        private static string ConvertJsonToString<T>(T json)
        {
            return JsonSerializer.Serialize(json) ?? "";
        }

        private T[] ConvertStringToJsonArray<T>(string s)
        {
            return JsonSerializer.Deserialize<T[]>(s) ?? [];
        }

        private double[] ConvertStringToDoubleArray(string str)
        {
            return str.Split(',').Select(double.Parse).ToArray() ?? [];
        }


        // Configure connection to the database file on disk
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured) { options.UseSqlite("Data Source=archive.sqlite"); }
            System.Diagnostics.Debug.WriteLine("Configured SQLite options with Data Source=archive.sqlite");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            System.Diagnostics.Debug.WriteLine("Start creating tables...");

            builder.Entity<User>()
                .HasKey(u => u.Id);

            builder.Entity<Archive>()
                .HasKey(a => a.Id);

            builder.Entity<Archive>()
                .Property(a => a.Equation)
                .HasColumnType("TEXT")
                .HasConversion(
                    toDb => string.Join(",", toDb),
                    fromDb => ConvertStringToDoubleArray(fromDb)
                );

            builder.Entity<Archive>()
                .Property(a => a.UserDataPoints)
                .HasColumnType("TEXT")
                .HasConversion(
                    toDb => ConvertJsonToString(toDb),
                    fromDb => ConvertStringToJsonArray<DataPoint>(fromDb)
                );

            builder.Entity<Archive>()
                .Property(a => a.FitDataPoints)
                .HasColumnType("TEXT")
                .HasConversion(
                    toDb => ConvertJsonToString(toDb),
                    fromDb => ConvertStringToJsonArray<DataPoint>(fromDb)
                );

            builder.Entity<User>()
                .HasMany(u => u.Archives)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            System.Diagnostics.Debug.WriteLine($"Tables created successfully");
        }
    }
}
