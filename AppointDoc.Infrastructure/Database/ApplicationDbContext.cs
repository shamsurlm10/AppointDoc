using AppointDoc.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace AppointDoc.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(pk => pk.UserId);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
        public DbSet<User> users { get; set; }
        public DbSet<Appointment> appointments { get; set; }
        public DbSet<Doctor> doctors { get; set; }
    }
}
