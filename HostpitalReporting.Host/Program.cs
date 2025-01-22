
using eCommerceApp.Application.Services.Implementation.Authentication;
using HospitalReporting.Application.Helper;
using HospitalReporting.Application.Services.ServicesImplementation;
using HospitalReporting.Application.Services.ServicesInterfaces;
using HospitalReporting.Domain.InterfacesRepository;
using HospitalReporting.Domain.InterfacesRepository.Authentication;
using HospitalReporting.Infrastructure.DependancyInjection;
using HospitalReporting.Infrastructure.ImplementationRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace HostpitalReporting.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddInfrastructureService(builder.Configuration);
            builder.Services.AddTransient<ISmsService, SmsService>();
            builder.Services.AddScoped<IPatientServices, PatientServices>();
            builder.Services.AddScoped<IReportService, ReportService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddAutoMapper(typeof(MapperProfile));
            builder.Services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Eneter Bearer Token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },new string[]{}
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
