using CinemaManager.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaManager.Data
{
    public class DBContext : DbContext
    {
        private const string ConfigurationFile = "appsettings.json";

        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(ConfigurationFile).Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DataBase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }

        public DbSet<MoviesModel> Movies { get; set; }
        public DbSet<TheatersModel> Theaters { get; set; }
        public DbSet<SessionModel> Sessions { get; set; }
        public DbSet<UserModel> Users { get; set; }
    }
}