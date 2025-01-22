using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Application.DTOs.Report
{
    public class BasePatient
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FullName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalNumberId { get; set; }
    }
}
