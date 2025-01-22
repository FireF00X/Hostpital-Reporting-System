using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;

namespace HospitalReporting.Application.Services.ServicesInterfaces
{
    public interface ISmsService
    {
        Task<MessageResource> SendMessage(string phoneNumber, string message);
    }
}
