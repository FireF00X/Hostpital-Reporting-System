using HospitalReporting.Application.DTOs.Report;
using HospitalReporting.Application.Services.ServicesInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HostpitalReporting.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController(IPatientServices _patient) : ControllerBase
    {
        [HttpDelete("DeletePatient")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeletePatient(string patientId)
        {
            var resutl = await _patient.DeletePatient(patientId);
            return resutl.Success ? Ok(resutl.Message) : BadRequest();
        }
        [HttpPut("UpdatePatient")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdatePatient(UpdatePatient patient)
        {
            var resutl = await _patient.UpdatePatientAsync(patient);
            return resutl.Success ? Ok(resutl.Message) : BadRequest();
        }
    }
}
