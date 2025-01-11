using AppointDoc.Application.Interfaces.Base;
using AppointDoc.Domain.DbModels;
using AppointDoc.Domain.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointDoc.Application.Interfaces
{
    public interface IAppointmentService : IManager<Appointment>
    {
        Task<bool> IsDoctorExist(Guid doctorId);
    }
}
