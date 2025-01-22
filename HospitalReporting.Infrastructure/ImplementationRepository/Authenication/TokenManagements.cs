using HospitalReporting.Domain.Entities.Identity;
using HospitalReporting.Domain.InterfacesRepository.Authentication;
using HospitalReporting.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Infrastructure.ImplementationRepository.Authenication
{
    public class TokenManagements(AppDbContext _context,
                                  IConfiguration _config) : ITokenManagements
    {
        public async Task<int> AddRefreshTokenAsync(string userId, string refreshToken)
        {
            await _context.RefreshTokens.AddAsync(new RefreshToken()
            {
                UserId = userId,
                Token = refreshToken
            });
            return await _context.SaveChangesAsync();
        }

        public string GenerateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expire = DateTime.UtcNow.AddHours(2);
            var token = new JwtSecurityToken(issuer: _config["JWT:Issure"],
                                             audience: _config["JWT:Audience"],
                                             expires: expire,
                                             claims: claims,
                                             signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetRefreshToken()
        {
            const int byteSize = 64;
            byte[] radnomBytes = new byte[byteSize];
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(radnomBytes);
            var token = Convert.ToBase64String(radnomBytes);
            return WebUtility.UrlEncode(token);
        }

        public IReadOnlyList<Claim> GetUserClaimsFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            if (jwtToken is not null)
                return jwtToken.Claims.ToList();
            else
                return [];
        }

        public async Task<string> GetUserIdByRefreshTokenAsync(string refreshToken)
            => (await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == refreshToken))!.UserId;

        public async Task<int> UpdateRefreshTokenAsync(string userId, string refreshToken)
        {
            var token = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.UserId == userId);
            if (token is null) return -1;
            token.Token = refreshToken;
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateRefreshTokenAsync(string refreshToken)
        {
            var token = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == refreshToken);
            return token != null;
        }
    }
}
