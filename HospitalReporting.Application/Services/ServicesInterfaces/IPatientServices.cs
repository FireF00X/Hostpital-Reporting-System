using HospitalReporting.Application.DTOs.Report;
using HospitalReporting.Application.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Application.Services.ServicesInterfaces
{
    public interface IPatientServices
    {
        Task<IReadOnlyList<GetPatient>> GetAllPatientsAsync();
        Task<GetPatient> GetPatientByIdAsync(string id);
        Task<ServiceResponse> AddPatientAsync(CreatePatient patient);
        Task<ServiceResponse> UpdatePatientAsync(UpdatePatient patient);
        Task<ServiceResponse> DeletePatient(string id);
        Task<GetPatient> GetPatientByNationalNumberId(string nationalNumberId);
    }
}
