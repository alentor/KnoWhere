using Database.Entities;
using Microsoft.EntityFrameworkCore;


namespace Database
{
    public class KnowWhereContext : DbContext
    { 
            public DbSet<User> Users { get; set; }
            public DbSet<UserSettings> UsersSettings { get; set; } 


        private string DatabasePath { get; set; }

            public KnowWhereContext()
            {

            }

            public KnowWhereContext(string databasePath)
            {
                DatabasePath = databasePath;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite($"Filename={DatabasePath}");
            } 
    }
}
