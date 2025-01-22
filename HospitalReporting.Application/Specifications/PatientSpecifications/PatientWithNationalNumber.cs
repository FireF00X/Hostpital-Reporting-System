using HospitalReporting.Domain.Entities;
using HospitalReporting.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Application.Specifications.PatientSpecifications
{
    public class PatientWithNationalNumber:BaseSpecifications<Patient>
    {
        public PatientWithNationalNumber(string nationalNumber) : base(p=>p.NationalNumberId == nationalNumber)
        {
            
        }
    }
}
