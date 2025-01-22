using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Application.DTOs.Identity
{
    public record LoginResponse(bool Success = false,
                                string? Message = null,
                                string? Token = null,
                                string? RefreshToken = null);
}
