using AppointDoc.Application.Repositories;
using AppointDoc.Domain.DbModels;
using AppointDoc.Domain.Dtos.Request;
using AppointDoc.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace AppointDoc.Infrastructure.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        #region Dependencies

        private readonly ApplicationDbContext _dbContext;

        public AuthenticationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region User Registration

        /// <summary>
        /// Registers a new user in the database.
        /// </summary>
        /// <param name="user">The registration details.</param>
        /// <returns>The newly created User object.</returns>
        public async Task<User> Register(LoginRegisterRequest user)
        {
            User newUser = new User
            {
                Username = user.Username,
                Password = user.Password,  // Password should already be hashed before calling this method.
            };

            await _dbContext.users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();

            return newUser;
        }

        #endregion

        #region Username Validation

        /// <summary>
        /// Validates whether the username exists in the database.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>The User object if found; otherwise, null.</returns>
        public async Task<User> UserNameValidation(string username)
        {
            return await _dbContext.users.FirstOrDefaultAsync(u => u.Username == username);
        }

        #endregion
    }
}
