using System.Security.Claims;
using AppointDoc.Api.Controllers;
using AppointDoc.Application.Interfaces;
using AppointDoc.Domain.Dtos.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Appointment.Test.Controllers
{
    [TestFixture]
    [TestOf(typeof(AppointmentController))]
    public class AppointmentControllerTest
    {
        private Mock<IAppointmentService> _appointmentServiceMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private AppointmentController _controller;

        [SetUp]
        public void SetUp()
        {
            _appointmentServiceMock = new Mock<IAppointmentService>();
            _controller = new AppointmentController(_appointmentServiceMock.Object);
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var context = new DefaultHttpContext();
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "test-user-id")
            }));
            context.User = claimsPrincipal;
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(context);

            _controller = new AppointmentController(_appointmentServiceMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };
        }
        [Test]
        public async Task Create_ValidRequest_ReturnsOk()
        {
            AppointmentRequestDto appointment = new AppointmentRequestDto
            {
                PatientName = "John Doe",
                PatientContactInformation = "1234567890",
                AppointmentDateTime = DateTime.UtcNow.AddDays(1),
                DoctorId = new Guid("9B4AFC8B-6AAD-4623-BC90-08A072523C57")
            };
            _appointmentServiceMock.Setup(x => x.IsDoctorExist(It.IsAny<Guid>())).ReturnsAsync(true); 
            _appointmentServiceMock.Setup(x => x.AddAsync(It.IsAny<AppointDoc.Domain.DbModels.Appointment>())).ReturnsAsync(true);
            var result = await _controller.CreateAppointment(appointment);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }
    }
}

