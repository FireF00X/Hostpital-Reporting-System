using HospitalReporting.Domain.Entities;
using HospitalReporting.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Application.Specifications.ReportSpecifications
{
    public class ReportWithPatient : BaseSpecifications<Report>
    {
        public ReportWithPatient(string reportId) : base(r=>r.Id == reportId)
        {
            Includes.Add(r => r.Patient);
        }
    }
}
