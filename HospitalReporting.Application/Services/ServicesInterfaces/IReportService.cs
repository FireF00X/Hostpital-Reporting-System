using HospitalReporting.Application.DTOs.Report;
using HospitalReporting.Application.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Application.Services.ServicesInterfaces
{
    public interface IReportService
    {
        Task<IReadOnlyList<GetReport>> GetAllReportsForPatientAsync(string patientId);
        Task<GetReport> GetReportForPatientAsync(string patientId);
        Task<ServiceResponse> AddReportAsync(CreateReport report);
        Task<ServiceResponse> UpdateReportAsync(UpdateReport report);
        Task<ServiceResponse> DeleteReport(string reportId);
        Task<ReportAndPatientDataDto> GetReportById(string reportId);
    }
}
