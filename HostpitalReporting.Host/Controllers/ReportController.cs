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
    public class ReportController : ControllerBase
    {
        private readonly IReportService _report;

        public ReportController(IReportService report)
        {
            _report = report;
        }

        [HttpPost("AddReport")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddReport(CreateReport report)
        {
            var result = await _report.AddReportAsync(report);
            return result.Success ? Ok(result.Message) : BadRequest();
        }

        [HttpDelete("RemoveReport")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> RemoveReport(string reportId)
        {
            var result = await _report.DeleteReport(reportId);
            return result.Success ? Ok(result.Message) : BadRequest();
        }

        [HttpGet("GetReport")]
        public async Task<IActionResult> GetRerportById (string reportId)
        {
            var result = await _report.GetReportById(reportId);
            return result is null ? BadRequest() : Ok(result);
        }

        [HttpPut("UpdateReport")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateRerport (UpdateReport report)
        {
            var result = await _report.UpdateReportAsync(report);
            return result is null ? BadRequest() : Ok(result.Message);
        }
    }
}
