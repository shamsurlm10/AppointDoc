using AppointDoc.Application.Interfaces.Base;
using AppointDoc.Domain.DbModels;

namespace AppointDoc.Application.Interfaces
{
    public interface IAppointmentService : IManager<Appointment>
    {
        Task<bool> IsDoctorExist(Guid doctorId);
    }
}
