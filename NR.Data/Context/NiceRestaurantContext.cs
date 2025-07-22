using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace NR.Data.Context
{
    public class NiceRestaurantContext : DbContext
    {
        public NiceRestaurantContext(DbContextOptions<NiceRestaurantContext> options)
            : base(options)
        {
        }

        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<GalleryImage> GalleryImages { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<ContactSubmission> ContactSubmissions { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed predefined admin
            modelBuilder.Entity<Admin>().HasData(new Admin
            {
                Id = 1,
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("P@ssw0rd123"),
                Role = "Admin"
            });
        }
    }
}
