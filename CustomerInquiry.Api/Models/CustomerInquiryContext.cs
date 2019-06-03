using Microsoft.EntityFrameworkCore;

namespace CustomerInquiry.Models
{
    public class CustomerInquiryContext : DbContext
    {
        public CustomerInquiryContext(DbContextOptions<CustomerInquiryContext> options)
            : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(x => x.Transactions)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerID);
            
            modelBuilder.Entity<Customer>()
                .HasIndex(x => x.Email)
                .HasName("IX_Customer_Email")
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
