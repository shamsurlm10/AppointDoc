using AppointDoc.Application.Interfaces;
using AppointDoc.Application.Services;
using AppointDoc.Domain.DbModels;
using AppointDoc.Domain.Dtos.Request;
using AppointDoc.Domain.Dtos.Response;
using Microsoft.AspNetCore.Mvc;

namespace AppointDoc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Dependencies
        private readonly IAuthenticationService _authService;
        public AuthController(IAuthenticationService authService, TokenService tokenService)
        {
            _authService = authService;
        }
        #endregion

        #region Registration

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="request">Registration request with username and password.</param>
        /// <returns>ActionResult with success or error message.</returns>
        
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register([FromBody] LoginRegisterRequest request)
        {
            try
            {
                // Check if the request body is null
                if (request == null)
                {
                    return BadRequest("Invalid request.");
                }
                // Validate password strength: at least 8 characters, one uppercase letter, and one digit
                if (request.Password.Length < 8 || !request.Password.Any(char.IsUpper) || !request.Password.Any(char.IsDigit))
                {
                    throw new ArgumentException("Password must be at least 8 characters long and include an uppercase letter and a number.");
                }
                // Check if username or password is empty or whitespace
                if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                {
                    throw new ArgumentException("Username and password cannot be empty.");
                }
                // Check if the username already exists in the database
                User existUser = await _authService.GetRegisteredUserByUsername(request.Username);
                if (existUser != null)
                {
                    return BadRequest("Username already registered");
                }
                // Register the new user using the authentication service
                User user = await _authService.Register(request);
                // Handle registration failures
                if (user == null)
                {
                    return StatusCode(500, "Registration failed. Please try again.");
                }
                // Return success response with the created user object
                return Ok(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        #endregion

        #region Login

        /// <summary>
        /// Authenticates a user and returns a JWT token.
        /// </summary>
        /// <param name="request">Login request with username and password.</param>
        /// <returns>JWT token if authentication is successful.</returns>
        
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] LoginRegisterRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Invalid request.");
            }
            AuthenticationResponse user = await _authService.ValidateUser(request);
            if (user.UserId == null)
            {
                return StatusCode(404, "Invalid username or password.");
            }
            return Ok(user);
        }
        #endregion
    }
}
