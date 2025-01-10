using AppointDoc.Domain.DbModels;
using AppointDoc.Domain.Dtos.Request;
using AppointDoc.Domain.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointDoc.Application.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<User> Register(LoginRegisterRequest user);
        Task<AuthenticationResponse> Login(LoginRegisterRequest request);
        Task<User> ValidateUser(LoginRegisterRequest request);
        Task<bool> UserNameValidation(string username);
    }
}
