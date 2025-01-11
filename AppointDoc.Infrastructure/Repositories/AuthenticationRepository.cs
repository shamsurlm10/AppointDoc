using AppointDoc.Application.Repositories;
using AppointDoc.Domain.DbModels;
using AppointDoc.Domain.Dtos.Request;
using AppointDoc.Domain.Dtos.Response;
using AppointDoc.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace AppointDoc.Infrastructure.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AuthenticationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<AuthenticationResponse> Login(LoginRegisterRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Register(LoginRegisterRequest user)
        {
            User newUser = new User
            {
                Username = user.Username,
                Password = user.Password,
            };
            await _dbContext.users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();
            return newUser;
        }

        public async Task<bool> UserNameValidation(string username)
        {
            User? user =  await _dbContext.users.FirstOrDefaultAsync(u=>u.Username == username);
            return user != null;
        }

        public async Task<User> ValidateUser(LoginRegisterRequest request)
        {
            User? user = await _dbContext.users
                .FirstOrDefaultAsync(u => u.Username == request.Username && u.Password == request.Password);
            return user;
        }
    }
}
