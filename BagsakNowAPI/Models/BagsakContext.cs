using Microsoft.EntityFrameworkCore;
namespace BagsakNowAPI.Models
{
    public class BagsakContext : DbContext
    {
        public BagsakContext(DbContextOptions<BagsakContext> options)
        : base(options) { }
        public DbSet<Member> Members { get; set; }
    }
}