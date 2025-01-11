using AppointDoc.Application.Interfaces;
using AppointDoc.Application.Repositories;
using AppointDoc.Domain.DbModels;
using AppointDoc.Domain.Dtos.Request;
using AppointDoc.Domain.Dtos.Response;
using Microsoft.AspNetCore.Identity;

namespace AppointDoc.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Dependencies

        private readonly IAuthenticationRepository _authRepo;
        private readonly TokenService _tokenService;
        private readonly IPasswordHasher<User> _passwordHasher;
        public AuthenticationService(IAuthenticationRepository authRepo, TokenService tokenService, IPasswordHasher<User> passwordHasher)
        {
            _authRepo = authRepo;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }

        #endregion

        #region User Validation

        /// <summary>
        /// Retrieves a registered user by username.
        /// </summary>
        /// <param name="username">The username to validate.</param>
        /// <returns>User object if found, otherwise null.</returns>
        public async Task<User> GetRegisteredUserByUsername(string username)
        {
            return await _authRepo.UserNameValidation(username);
        }

        #endregion

        #region Registration

        /// <summary>
        /// Registers a new user with hashed password.
        /// </summary>
        /// <param name="request">User registration details.</param>
        /// <returns>The newly created User object.</returns>
        /// <exception cref="Exception">Throws if registration fails.</exception>
        public async Task<User> Register(LoginRegisterRequest request)
        {
            try
            {
                User user = new User
                {
                    Username = request.Username
                };
                // Hash the password before saving
                request.Password = _passwordHasher.HashPassword(user, request.Password);
                return await _authRepo.Register(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Authentication

        /// <summary>
        /// Validates user credentials and generates a JWT token if successful.
        /// </summary>
        /// <param name="request">Login request containing username and password.</param>
        /// <returns>AuthenticationResponse containing JWT token and user details.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when validation fails.</exception>

        public async Task<AuthenticationResponse> ValidateUser(LoginRegisterRequest request)
        {
            // Validate username
            User validateByUserName = await _authRepo.UserNameValidation(request.Username);
            if (validateByUserName == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }
            // Validate password
            var verificationResult = _passwordHasher.VerifyHashedPassword(validateByUserName, validateByUserName.Password, request.Password);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }
            // Generate JWT token upon successful validation
            AuthenticationResponse authentication = new AuthenticationResponse
            {
                Token = _tokenService.GenerateJwtToken(validateByUserName),
                UserId = validateByUserName.UserId.ToString(),   
                IssueDate = DateTime.UtcNow,
            };
            return authentication;

            #endregion
        }
    }
}
