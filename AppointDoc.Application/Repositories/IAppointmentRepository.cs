using AppointDoc.Application.Repositories.Base;
using AppointDoc.Domain.DbModels;

namespace AppointDoc.Application.Repositories
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<bool> IsDoctorExist(Guid doctorId);
    }
}
