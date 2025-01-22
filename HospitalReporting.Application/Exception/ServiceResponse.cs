using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Application.Exception
{
    public record ServiceResponse (bool Success=false,string? Message =null);
    
}
