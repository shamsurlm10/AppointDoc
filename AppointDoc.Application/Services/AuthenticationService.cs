using AppointDoc.Application.Interfaces;
using AppointDoc.Application.Interfaces.Base;
using AppointDoc.Application.Repositories;
using AppointDoc.Domain.DbModels;
using AppointDoc.Domain.Dtos.Request;
using AppointDoc.Domain.Dtos.Response;

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
                return await _authRepo.Register(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AuthenticationResponse> ValidateUser(LoginRegisterRequest request)
        {
            User user = await _authRepo.ValidateUser(request);
            AuthenticationResponse authentication = new AuthenticationResponse
            {
                Token = _tokenService.GenerateJwtToken(user),
                UserId = user.UserId,   
                IssueDate = DateTime.UtcNow,
            };
            return authentication;

        }
    }
}
