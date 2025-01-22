using HospitalReporting.Application.DTOs.Identity;
using HospitalReporting.Application.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Application.Services.ServicesInterfaces
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse> CreateUser(CreateUser user);
        Task<LoginResponse> LoginUser(LoginUser user);
        Task<LoginResponse> ReviveToken(string refreshToken);
        Task<ServiceResponse> LoginNewCommer(LoginForFirstTime data);
    }

}
