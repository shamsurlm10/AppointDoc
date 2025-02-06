using AppointDoc.Api.Controllers;
using AppointDoc.Application.Interfaces;
using AppointDoc.Domain.Dtos.Request;
using AppointDoc.Domain.DbModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using AppointDoc.Application.Services;
using AppointDoc.Domain.Dtos.Response;
using Microsoft.Extensions.Configuration;

namespace Appointment.Test.Controllers
{
    [TestFixture]
    [TestOf(typeof(AuthController))]
    public class AuthControllerTest
    {
        private Mock<IAuthenticationService> _authServiceMock;
        private TokenService _tokenService;
        private AuthController _controller;

        [SetUp]
        public void SetUp()
        {
            _authServiceMock = new Mock<IAuthenticationService>();

            // Create a mock configuration for TokenService
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.SetupGet(config => config["Jwt:Secret"]).Returns("your_jwt_secret");
            configurationMock.SetupGet(config => config["Jwt:Issuer"]).Returns("your_jwt_issuer");
            configurationMock.SetupGet(config => config["Jwt:Audience"]).Returns("your_jwt_audience");

            _tokenService = new TokenService(configurationMock.Object);
            _controller = new AuthController(_authServiceMock.Object, _tokenService);
        }

        [Test]
        public async Task Register_ValidRequest_ReturnsOk()
        {
            // Arrange
            var request = new LoginRegisterRequest
            {
                Username = "testuser",
                Password = "Test@1234"
            };
            var user = new User
            {
                Username = "testuser"
            };
            _authServiceMock.Setup(service => service.GetRegisteredUserByUsername(request.Username)).ReturnsAsync((User)null);
            _authServiceMock.Setup(service => service.Register(request)).ReturnsAsync(user);

            // Act
            var result = await _controller.Register(request);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.Not.Null);
            Assert.That(okResult.Value, Is.InstanceOf<User>());
            var returnedUser = okResult.Value as User;
            Assert.That("testuser", Is.EqualTo(returnedUser.Username));
        }
        [Test]
        public async Task Login_ValidRequest_ReturnsOk()
        {
            // Arrange
            var request = new LoginRegisterRequest
            {
                Username = "testuser",
                Password = "Test@1234"
            };
            var authResponse = new AuthenticationResponse
            {
                Token = "valid_jwt_token",
                UserId = "1",
                IssueDate = DateTime.UtcNow
            };
            _authServiceMock.Setup(service => service.ValidateUser(request)).ReturnsAsync(authResponse);

            // Act
            var result = await _controller.Login(request);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.Not.Null);
            Assert.That(okResult.Value, Is.InstanceOf<AuthenticationResponse>());
            var returnedResponse = okResult.Value as AuthenticationResponse;
            Assert.That("valid_jwt_token", Is.EqualTo(returnedResponse.Token));
            Assert.That("1", Is.EqualTo(returnedResponse.UserId));
        }
    }
}