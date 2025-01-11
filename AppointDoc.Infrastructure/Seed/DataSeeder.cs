using AppointDoc.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace AppointDoc.Infrastructure.Seed
{
    public static class DataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    DoctorId = Guid.NewGuid(),
                    DoctorName = "Dr. Shamsur"
                }
            );
        }
    }
}
