using KnoWhere.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace KnoWhere.API.Data
{
    public class KnoWhereContext : DbContext
    {
        public KnoWhereContext(DbContextOptions<KnoWhereContext> options)
            : base(options)
        {
        }

        public DbSet<PlaceBucket> PlacesBucket { get; set; }
    }
}