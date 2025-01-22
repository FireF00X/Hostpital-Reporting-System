using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Domain.Entities
{
    public class Report
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public int DaysOf {  get; set; }
        public string PatientId { get; set; }
        public Patient? Patient { get; set; }
    }
}
