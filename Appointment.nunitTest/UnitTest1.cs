using Newtonsoft.Json;
using System.Text;

namespace Appointment.nunitTest
{
    public class Tests
    {
        [SetUp]
        public void SetUp()
        {
            _client = new HttpClient();
            _client.BaseAddress = new System.Uri("https://localhost:7101/api/Appointment");
        }

        [Test]
        public async Task Register_ShouldReturnSuccess_ForValidRequest()
        {
            //Arrange
            var user = new
            {
                Password = "54238179M"
            };
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            //Act
            var response = await _client.PostAsync("/Register", content);
            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            var responseBody = await response.Content.ReadAsStringAsync();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        }

        [Test]
        public async Task Register_ShouldReturnBadRequest_ForPasswordTooShort()
        {
            // Arrange
            var user = new
            {
                Username = "testUser",
                Password = "short1"
            };
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/Register", content);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            var responseBody = await response.Content.ReadAsStringAsync();
            responseBody.Should().Contain("Password must be at least 8 characters long");
        }

        [Test]
        public async Task Register_ShouldReturnBadRequest_ForPasswordNoUppercase()
        {
            // Arrange
            var user = new
            {
                Username = "testUser",
                Password = "lowercase1"
            };
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/Register", content);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            var responseBody = await response.Content.ReadAsStringAsync();
            responseBody.Should().Contain("Password must include an uppercase letter");
        }
    }
}