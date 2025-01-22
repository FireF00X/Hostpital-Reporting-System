using AutoMapper;
using HospitalReporting.Application.DTOs.Identity;
using HospitalReporting.Application.DTOs.Report;
using HospitalReporting.Domain.Entities;
using HospitalReporting.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Application.Helper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreatePatient, Patient>();
            CreateMap<Patient, GetPatient>();
            CreateMap<UpdatePatient, Patient>();

            CreateMap<CreateReport, Report>();
            CreateMap<Report, GetReport>();
            CreateMap<Report, ReportAndPatientDataDto>();
            CreateMap<Report, UpdateReport>().ReverseMap();

            CreateMap<CreateUser, AppUser>();
            CreateMap<LoginUser, AppUser>().ReverseMap();
        }
    }
}
