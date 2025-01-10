using AppointDoc.Application.Repositories;
using AppointDoc.Domain.DbModels;
using AppointDoc.Infrastructure.Database;
using AppointDoc.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointDoc.Infrastructure.Repositories
{
    public class AppointmentRepository : EfRepository<Appointment>, IAppointmentRepository
    {
        private readonly ApplicationDbContext _db;
        public AppointmentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
