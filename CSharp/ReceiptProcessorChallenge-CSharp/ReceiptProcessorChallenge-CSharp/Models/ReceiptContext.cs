using Microsoft.EntityFrameworkCore;

namespace ReceiptProcessorChallenge_CSharp.Models
{
    public class ReceiptContext : DbContext
    {
        public ReceiptContext(DbContextOptions<ReceiptContext> options) : base(options) { }

        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Item>().HasNoKey();
        }
    }
}
