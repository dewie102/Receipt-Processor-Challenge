using Microsoft.EntityFrameworkCore;

namespace ReceiptProcessorChallenge_CSharp.Models
{
    public class PointContext : DbContext
    {
        public PointContext(DbContextOptions<PointContext> options) : base(options) { }

        public DbSet<Point> Points { get; set; }
    }
}
