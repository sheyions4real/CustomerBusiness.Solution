using Microsoft.EntityFrameworkCore;

namespace Customer.Service.API.Data.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Schedule> Schedules { get; set; }  
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Sale> Sales { get; set; }
        
        public DbSet<SaleItem> SaleItems { get; set; }

        public DbSet<User> Users { get; set; }
        
        public DbSet<LoginToken> LoginTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional configurations can be added here if needed


            // Cascade delete rules (optional)
            modelBuilder.Entity<Business>()
                .HasMany(b => b.Customers)
                .WithOne(c => c.Business)
                .OnDelete(DeleteBehavior.Cascade);

            // SECONDARY ADDRESSES (One-to-Many)
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Addresses)
                .WithOne(a => a.Customer)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SaleItem>()
                .Property(si => si.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Sale>()
                .Property(s => s.TotalAmount)
                .HasPrecision(18, 2);

        }
        
    }
}
