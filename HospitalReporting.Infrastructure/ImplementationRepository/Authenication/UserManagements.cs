using AutoMapper;
using HospitalReporting.Domain.Entities.Identity;
using HospitalReporting.Domain.InterfacesRepository;
using HospitalReporting.Domain.InterfacesRepository.Authentication;
using HospitalReporting.Domain.Specifications;
using HospitalReporting.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Infrastructure.ImplementationRepository.Authenication
{
    public class UserManagements(UserManager<AppUser> _userManager,
                                   IUnitOfWork _repo,
                                   IRoleManagements _roleManager) : IUserManagements
    {
        public async Task<bool> CreateUserAsync(AppUser user)
        {
            var userExist = await GetUserByEmailAsync(user.Email!);
            if (userExist != null) return false;
            var result = await _userManager.CreateAsync(user!, user.PasswordHash!);
            return result.Succeeded;
        }

        public async Task<List<Claim>> GetAllUserClaimsAsync(string email)
        {
            var user = await GetUserByEmailAsync(email);
            var role = await _roleManager.GetUserRoleAsync(email);

            List<Claim> claims = new List<Claim>()
            {
                new Claim("fullName",user.FullName!),
                new Claim(ClaimTypes.NameIdentifier,user.Id!),
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.Role,role),
            };
            return claims;
        }

        public async Task<IReadOnlyList<AppUser>> GetAllUsersAsync()
        {
            var users = await _repo.CreateRepo<AppUser>().GetAllAsync();
            return users;
        }

        public async Task<AppUser?> GetUserByEmailAsync(string email) => await _userManager.FindByEmailAsync(email);

        public Task<AppUser?> GetUserByIdAsync(string id) => _userManager.FindByIdAsync(id);

        public async Task<bool> LoginUserAsync(AppUser user)
        {
            var userLogged = await GetUserByEmailAsync(user.Email!);
            if (userLogged == null) return false;

            var roleName = await _roleManager.GetUserRoleAsync(user.Email!);
            if (string.IsNullOrEmpty(roleName)) return false;

            return await _userManager.CheckPasswordAsync(userLogged, user.PasswordHash!);
        }

        public async Task<int> RemoveUserByEmailAsync(string email)
        {
            var user = await _repo.CreateRepo<AppUser>().GetByIdWithSpecsAsync(new BaseSpecifications<AppUser>(u=>u.Email == email));
            _repo.CreateRepo<AppUser>().Delete(user.Id);
            return await _repo.CompleteAsync();
        }
    }
}
