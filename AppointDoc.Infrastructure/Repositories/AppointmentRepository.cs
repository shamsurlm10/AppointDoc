using AppointDoc.Application.Repositories;
using AppointDoc.Domain.DbModels;
using AppointDoc.Infrastructure.Database;
using AppointDoc.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AppointDoc.Infrastructure.Repositories
{
    public class AppointmentRepository : EfRepository<Appointment>, IAppointmentRepository
    {
        #region Dependencies

        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentRepository"/> class.
        /// </summary>
        /// <param name="db">The database context.</param>
        public AppointmentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        #endregion

        #region Doctor Validation

        /// <summary>
        /// Checks if a doctor exists in the database by their ID.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        /// <returns>True if the doctor exists; otherwise, false.</returns>
        public async Task<bool> IsDoctorExist(Guid doctorId)
        {
            return await _db.doctors.AnyAsync(d => d.DoctorId == doctorId);
        }

        #endregion
    }
}
