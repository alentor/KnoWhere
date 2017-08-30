using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KnoWhere.API.Data
{
    class KnoWhereContextFactory : IDesignTimeDbContextFactory<KnoWhereContext>
    {
        public KnoWhereContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<KnoWhereContext>();
            builder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=KnoWhereDB;Trusted_Connection=True;");
            return new KnoWhereContext(builder.Options);
        }
    }
}