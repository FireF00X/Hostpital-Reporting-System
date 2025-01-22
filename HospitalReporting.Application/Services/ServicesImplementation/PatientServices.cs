using AutoMapper;
using HospitalReporting.Application.DTOs.Report;
using HospitalReporting.Application.Exception;
using HospitalReporting.Application.Services.ServicesInterfaces;
using HospitalReporting.Application.Specifications.PatientSpecifications;
using HospitalReporting.Domain.Entities;
using HospitalReporting.Domain.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Application.Services.ServicesImplementation
{
    public class PatientServices(IUnitOfWork _repo, IMapper _mapper) : IPatientServices
    {
        public async Task<ServiceResponse> AddPatientAsync(CreatePatient patient)
        {
            var mappedPatient = _mapper.Map<Patient>(patient);
            var addPatient = await _repo.CreateRepo<Patient>().AddAsync(mappedPatient);
            var result = await _repo.CompleteAsync();
            if (result <= 0) return new ServiceResponse(Message: "Faild to Add Patient");
            return new ServiceResponse(true, "Patient Add Successfully");
        }

        public  async Task<ServiceResponse> DeletePatient(string id)
        {
            await _repo.CreateRepo<Patient>().Delete(id);
            var result = await _repo.CompleteAsync();
            return result <=0 ? new ServiceResponse(Message: $"No Patient found with this Id: {id}"):            
            new ServiceResponse(true, "Patient Deleted Successfully");
        }

        public async Task<GetPatient> GetPatientByIdAsync(string id)
        {
            var patient =await _repo.CreateRepo<Patient>().GetByIdAsync(id);
            if (patient is null) return null;
            var mappedPatient = _mapper.Map<GetPatient>(patient);
            return mappedPatient;
        }

        public async Task<IReadOnlyList<GetPatient>> GetAllPatientsAsync()
        {
            var patients = await _repo.CreateRepo<Patient>().GetAllAsync();
            if (patients.Count() <= 0) return [];
            return _mapper.Map<IReadOnlyList<GetPatient>>(patients);
        }

        public async Task<ServiceResponse> UpdatePatientAsync(UpdatePatient patient)
        {            
            var mappedPatient = _mapper.Map<Patient>(patient);
            var updatePatient =await _repo.CreateRepo<Patient>().UpdateAsync(mappedPatient);
            var result = await _repo.CompleteAsync();
            return result <= 0 ? new ServiceResponse(Message: "Faild to update the Patient") :
               new ServiceResponse(true, "Patient Updated successfully");
        }

        public async Task<GetPatient> GetPatientByNationalNumberId(string nationalNumberId)
        {
            var patient = await _repo.CreateRepo<Patient>().GetByIdWithSpecsAsync(new PatientWithNationalNumber(nationalNumberId));
            if (patient is null) return null;
            return _mapper.Map<GetPatient>(patient);
        }
    }
}
