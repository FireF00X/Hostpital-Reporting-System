using System.ComponentModel.DataAnnotations;

namespace HospitalReporting.Application.DTOs.Report
{
    public class GetPatient : BasePatient
    {
        public ICollection<GetReport> Reports { get; set; } = new HashSet<GetReport>();
    }
}
