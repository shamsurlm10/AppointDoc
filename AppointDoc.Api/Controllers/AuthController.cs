﻿using AppointDoc.Application.Interfaces;
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
        private readonly IAuthenticationService _authService;
        public AuthController(IAuthenticationService authService, TokenService tokenService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register([FromBody] LoginRegisterRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request.");
            }
            if (request.Password.Length < 8 || !request.Password.Any(char.IsUpper) || !request.Password.Any(char.IsDigit))
            {
                throw new ArgumentException("Password must be at least 8 characters long and include an uppercase letter and a number.");
            }
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                throw new ArgumentException("Username and password cannot be empty.");
            }
            User existUser = await _authService.GetRegisteredUserByUsername(request.Username);
            if (existUser!=null)
            {
                return BadRequest("Username already registered");
            }
            User user = await _authService.Register(request);
            if (user == null)
            {
                return NotFound("Registration failed. Please try again.");
            }
            return Ok(user);
        }

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
    }
}
