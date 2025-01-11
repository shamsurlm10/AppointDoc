using AppointDoc.Application.Interfaces;
using AppointDoc.Application.Repositories;
using AppointDoc.Domain.DbModels;
using AppointDoc.Domain.Dtos.Request;
using AppointDoc.Domain.Dtos.Response;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace AppointDoc.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authRepo;
        private readonly TokenService _tokenService;
        private readonly IPasswordHasher<User> _passwordHasher;
        public AuthenticationService(IAuthenticationRepository authRepo, TokenService tokenService, IPasswordHasher<User> passwordHasher)
        {
            _authRepo = authRepo;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> GetRegisteredUserByUsername(string username)
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
                User user = new User
                {
                    Username = request.Username
                };
                request.Password = _passwordHasher.HashPassword(user, request.Password);
                return await _authRepo.Register(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AuthenticationResponse> ValidateUser(LoginRegisterRequest request)
        {
            User validateByUserName = await _authRepo.UserNameValidation(request.Username);
            if (validateByUserName == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }
            var verificationResult = _passwordHasher.VerifyHashedPassword(validateByUserName, validateByUserName.Password, request.Password);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }
            AuthenticationResponse authentication = new AuthenticationResponse
            {
                Token = _tokenService.GenerateJwtToken(validateByUserName),
                UserId = validateByUserName.UserId.ToString(),   
                IssueDate = DateTime.UtcNow,
            };
            return authentication;

        }
    }
}
