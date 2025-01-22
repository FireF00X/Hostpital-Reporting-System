using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Domain.InterfacesRepository.Authentication
{
    public interface ITokenManagements
    {
        IReadOnlyList<Claim> GetUserClaimsFromToken(string token);
        string GetRefreshToken();
        Task<bool> ValidateRefreshTokenAsync(string refreshToken);
        Task<string> GetUserIdByRefreshTokenAsync(string refreshToken);
        Task<int> AddRefreshTokenAsync(string userId, string refreshToken);
        Task<int> UpdateRefreshTokenAsync(string userId, string refreshToken);
        string GenerateToken(List<Claim> claims);
    }
}
