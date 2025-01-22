using HospitalReporting.Domain.Entities.Identity;
using HospitalReporting.Domain.InterfacesRepository.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Infrastructure.ImplementationRepository.Authenication
{
    public class RoleManagements(UserManager<AppUser> _user) : IRoleManagements
    {
        public async Task<bool> AddRoleToUserAsync(AppUser user, string roleName)
        {
            var result = await _user.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public async Task<string?> GetUserRoleAsync(string email)
        {
            var user = await _user.FindByEmailAsync(email);
            return (await _user.GetRolesAsync(user!)).FirstOrDefault();
        }
    }
}
