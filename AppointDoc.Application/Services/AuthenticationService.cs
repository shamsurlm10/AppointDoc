using AppointDoc.Application.Interfaces;
using AppointDoc.Application.Interfaces.Base;
using AppointDoc.Application.Repositories;
using AppointDoc.Domain.DbModels;
using AppointDoc.Domain.Dtos.Request;
using AppointDoc.Domain.Dtos.Response;
using System.Security.Cryptography;
using System.Text;

namespace AppointDoc.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authRepo;
        private readonly TokenService _tokenService;
        public AuthenticationService(IAuthenticationRepository authRepo, TokenService tokenService)
        {
            _authRepo = authRepo;
            _tokenService = tokenService;
        }

        public async Task<bool> IsAlreadyRegisteredUsername(string username)
        {
            return await _authRepo.UserNameValidation(username);
        }

        public async Task<AuthenticationResponse> Login(LoginRegisterRequest request)
        {
            return await _authRepo.Login(request);
        }

        public async Task<User> Register(LoginRegisterRequest request)
        {
            try
            {
                request.Password = HashPassword(request.Password);
                return await _authRepo.Register(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AuthenticationResponse> ValidateUser(LoginRegisterRequest request)
        {
            request.Password = HashPassword(request.Password);
            User user = await _authRepo.ValidateUser(request);
            if(user == null)
            {
                return new AuthenticationResponse();
            }
            AuthenticationResponse authentication = new AuthenticationResponse
            {
                Token = _tokenService.GenerateJwtToken(user),
                UserId = user.UserId.ToString(),   
                IssueDate = DateTime.UtcNow,
            };
            return authentication;

        }
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
