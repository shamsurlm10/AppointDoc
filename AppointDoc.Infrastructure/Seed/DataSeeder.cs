using AppointDoc.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace AppointDoc.Infrastructure.Seed
{
    public static class DataSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    DoctorId = Guid.NewGuid(),
                    DoctorName = "Dr. Shamsur"
                },
                new Doctor
                {
                    DoctorId = Guid.NewGuid(),
                    DoctorName = "Dr. Mawa"
                },
                new Doctor
                {
                    DoctorId = Guid.NewGuid(),
                    DoctorName = "Dr. Ashiq"
                },
                new Doctor
                {
                    DoctorId = new Guid("9B4AFC8B-6AAD-4623-BC90-08A072523C57"),
                    DoctorName = "Dr. Alam"
                }
            );
        }
    }
}
