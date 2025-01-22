using HospitalReporting.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Domain.InterfacesRepository.Authentication
{
    public interface IUserManagements
    {
        Task<bool> CreateUserAsync(AppUser user);
        Task<bool> LoginUserAsync(AppUser user);
        Task<AppUser?> GetUserByEmailAsync(string email);
        Task<AppUser?> GetUserByIdAsync(string id);
        Task<IReadOnlyList<AppUser>> GetAllUsersAsync();
        Task<List<Claim>> GetAllUserClaimsAsync(string email);
        Task<int> RemoveUserByEmailAsync(string email);
    }
}
