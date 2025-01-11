using AppointDoc.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
