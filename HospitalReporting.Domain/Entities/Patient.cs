using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Domain.Entities
{
    public class Patient
    {
        [Key]
        public string Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalNumberId { get; set; }
        public ICollection<Report> Reports { get; set; } = new HashSet<Report>();
    }
}
