using AppointDoc.Domain.DbModels;
using AppointDoc.Infrastructure.Seed;
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
            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);

                entity.HasIndex(u => u.Username)
                      .IsUnique();

                entity.Property(u => u.Username)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(u => u.Password)
                      .HasMaxLength(100)
                      .IsRequired();
            });

            // Configure Doctor entity
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(d => d.DoctorId);

                entity.Property(d => d.DoctorName)
                      .HasMaxLength(100)
                      .IsRequired();
            });

            // Configure Appointment entity
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(a => a.AppointmentId);

                entity.Property(a => a.PatientName)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(a => a.PatientContactInformation)
                      .HasMaxLength(15)
                      .IsRequired();
            });

        }
        public DbSet<User> users { get; set; }
        public DbSet<Appointment> appointments { get; set; }
        public DbSet<Doctor> doctors { get; set; }
    }
}
