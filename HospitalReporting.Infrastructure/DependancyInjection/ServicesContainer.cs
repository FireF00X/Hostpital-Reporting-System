using HospitalReporting.Domain.Entities.Identity;
using HospitalReporting.Domain.InterfacesRepository;
using HospitalReporting.Domain.InterfacesRepository.Authentication;
using HospitalReporting.Infrastructure.Context;
using HospitalReporting.Infrastructure.ImplementationRepository;
using HospitalReporting.Infrastructure.ImplementationRepository.Authenication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HospitalReporting.Infrastructure.DependancyInjection
{
    public static class ServicesContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection _services, IConfiguration _config)
        {
            _services.AddDbContext<AppDbContext>(option =>
            option.UseSqlServer(_config.GetConnectionString("DefaultConnection")
            ));
            _services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));
            _services.AddScoped<IUserManagements, UserManagements>();
            _services.AddScoped<IRoleManagements, RoleManagements>();
            _services.AddScoped<ITokenManagements, TokenManagements>();

            _services.AddDefaultIdentity<AppUser>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 1;
            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            _services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _config["JWT:Issure"],
                    ValidAudience = _config["JWT:Audience"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:key"]!))
                };
            });
            return _services;
        }
    }
}
