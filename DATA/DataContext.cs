using BackEndStructuer.Entities;
using Gaz_BackEnd.Entities;
using Microsoft.EntityFrameworkCore;
using File = Gaz_BackEnd.Entities.File;

namespace BackEndStructuer.DATA
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }


        public DbSet<Address> Addresses { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<Otp> Otps { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Document> Documents { get; set; }
        
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderProducts)
            .WithOne(op => op.Order)
            .HasForeignKey(op => op.OrderId);
            
            modelBuilder.Entity<Cart>()
            .HasMany(o => o.CartProducts)
            .WithOne(op => op.Cart)
            .HasForeignKey(op => op.CartId);
            // new DbInitializer(modelBuilder).Seed();

        }
        
    }
}