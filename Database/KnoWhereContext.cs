using Database.Entities;
using Microsoft.EntityFrameworkCore;


namespace Database
{
    public class KnoWhereContext : DbContext
    { 
            public DbSet<User> Users { get; set; }
            public DbSet<UserSettings> UsersSettings { get; set; }
            public DbSet<Radius> Ranges { get; set; }


        private string DatabasePath { get; set; }

            public KnoWhereContext()
            {

            }

            public KnoWhereContext(string databasePath)
            {
                DatabasePath = databasePath;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite($"Filename={DatabasePath}");
            } 
    }
}
