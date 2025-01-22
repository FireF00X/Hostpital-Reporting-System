using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Application.DTOs.Report
{
    public class ReportAndPatientDataDto
    {
        public string Name { get; set; }
        public int DaysOf { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalNumberId { get; set; }
    }
}
