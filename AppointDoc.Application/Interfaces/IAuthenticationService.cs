using AppointDoc.Domain.DbModels;
using AppointDoc.Domain.Dtos.Request;
using AppointDoc.Domain.Dtos.Response;

namespace AppointDoc.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User> Register (LoginRegisterRequest user);
        Task<AuthenticationResponse> ValidateUser(LoginRegisterRequest request);
        Task<User> GetRegisteredUserByUsername(string username);
    }
}
