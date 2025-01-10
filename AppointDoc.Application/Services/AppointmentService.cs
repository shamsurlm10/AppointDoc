using AppointDoc.Application.Interfaces;
using AppointDoc.Application.Repositories;
using AppointDoc.Application.Services.Base;
using AppointDoc.Domain.DbModels;

namespace AppointDoc.Application.Services
{
    public class AppointmentService : Manager<Appointment>, IAppointmentService
    {
        private readonly IAppointmentRepository _repo;
        public AppointmentService(IAppointmentRepository repo) : base(repo)
        {
            _repo = repo;
        }
    }
}
