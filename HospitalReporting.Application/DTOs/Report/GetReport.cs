using System.ComponentModel.DataAnnotations;

namespace HospitalReporting.Application.DTOs.Report
{
    public class GetReport : BaseReport
    {
        public string Id { get; set; } 
        public GetPatient? Patient { get; set; }
    }
}
