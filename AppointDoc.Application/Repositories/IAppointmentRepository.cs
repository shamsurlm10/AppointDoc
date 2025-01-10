using AppointDoc.Application.Repositories.Base;
using AppointDoc.Domain.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointDoc.Application.Repositories
{
    public interface IAppointmentRepository:IRepository<Appointment>
    {
    }
}
