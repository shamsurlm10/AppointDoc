using AppointDoc.Domain.DbModels;
using AppointDoc.Domain.Dtos.Request;

namespace AppointDoc.Application.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<User> Register(LoginRegisterRequest user);
        Task<User> UserNameValidation(string username);
    }
}
