using HospitalReporting.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Domain.InterfacesRepository.Authentication
{
    public interface IRoleManagements
    {
        Task<string?> GetUserRoleAsync(string email);
        Task<bool> AddRoleToUserAsync(AppUser user, string roleName);
    }
}
