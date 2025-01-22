using AutoMapper;
using HospitalReporting.Application.DTOs.Report;
using HospitalReporting.Application.Exception;
using HospitalReporting.Application.Services.ServicesInterfaces;
using HospitalReporting.Application.Specifications.ReportSpecifications;
using HospitalReporting.Domain.Entities;
using HospitalReporting.Domain.InterfacesRepository;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Application.Services.ServicesImplementation
{
    public class ReportService(IUnitOfWork _repo,
                               IMapper _mapper,
                               ISmsService _sendMessage,
                               IPatientServices _patient) : IReportService
    {
        public async Task<ServiceResponse> AddReportAsync(CreateReport report)
        {
            var getPatientByNationalNumberId = await _patient.GetPatientByNationalNumberId(report.PatientNationalNumberId);

            if(getPatientByNationalNumberId == null)
            {
                var newPatient = new CreatePatient()
                {                    
                    FullName= report.PatientFullName,
                    Age = report.PatientAge,
                    PhoneNumber = report.PatientPhoneNumber,
                    NationalNumberId = report.PatientNationalNumberId
                };
                var addPatientResult = await _patient.AddPatientAsync(newPatient);
                report.PatientId =  addPatientResult.Success ? newPatient.Id.ToString() : null;
            }
            else
            {
                report.PatientId = getPatientByNationalNumberId.Id;
            }

            var mappedReport = new Report
            {
                Id = Guid.NewGuid().ToString(),
                DaysOf = report.DaysOf,
                Name = report.Name,
                PatientId = report.PatientId
            };

            var addReport = await _repo.CreateRepo<Report>().AddAsync(mappedReport);
            var result = await _repo.CompleteAsync();

            if (result <= 0) return new ServiceResponse(Message: "Faild to add this report");
            await _sendMessage.SendMessage($"+2{report.PatientPhoneNumber}", $"Your Report Code is ( {mappedReport.Id.ToString()} )");
            return new ServiceResponse(true, "Report Created Successfully");
        }

        public async Task<ServiceResponse> DeleteReport(string reportId)
        {
            var reportToDelete =await _repo.CreateRepo<Report>().Delete(reportId);
            var result = await _repo.CompleteAsync();
            return result <= 0 ? new ServiceResponse(Message: "Faild to Delete This Report") :
                new ServiceResponse(true, "Report Deleted Successfully");
        }

        public Task<IReadOnlyList<GetReport>> GetAllReportsForPatientAsync(string patientId)
        {
            throw new NotImplementedException();
        }

        public async Task<ReportAndPatientDataDto> GetReportById(string reportId)
        {            
            var getReport = await _repo.CreateRepo<Report>().GetByIdWithSpecsAsync(new ReportWithPatient(reportId));
            var mappedReport = new ReportAndPatientDataDto()
            {
                Name = getReport.Name,
                Age = getReport.Patient.Age,
                DaysOf =getReport.DaysOf,
                FullName = getReport.Patient.FullName,
                NationalNumberId = getReport.Patient.NationalNumberId,
                PhoneNumber = getReport.Patient.PhoneNumber
            };
            if (mappedReport is null) return null;
            return mappedReport;
        }

        public Task<GetReport> GetReportForPatientAsync(string patientId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse> UpdateReportAsync(UpdateReport report)
        {
            var mappedReport = _mapper.Map<Report>(report);
            await _repo.CreateRepo<Report>().UpdateAsync(mappedReport);
            var result = await _repo.CompleteAsync();
            return result <= 0 ? new ServiceResponse(Message: "Faild To Update Report") :
                new ServiceResponse(true, "Report Updated Successfully");
        }

    }
}
