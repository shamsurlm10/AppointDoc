using AppointDoc.Application.Interfaces.Base;
using AppointDoc.Domain.DbModels;
using AppointDoc.Domain.Dtos.Request;
using AppointDoc.Domain.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointDoc.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User> Register (LoginRegisterRequest user);
        Task<AuthenticationResponse> Login(LoginRegisterRequest request);
        Task<AuthenticationResponse> ValidateUser(LoginRegisterRequest request);
        Task<User> GetRegisteredUserByUsername(string username);
    }
}
