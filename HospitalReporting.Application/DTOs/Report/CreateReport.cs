namespace HospitalReporting.Application.DTOs.Report
{
    public class CreateReport : BaseReport
    {
        public string PatientFullName { get; set; }
        public int PatientAge { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string PatientNationalNumberId { get; set; }
    }
}
