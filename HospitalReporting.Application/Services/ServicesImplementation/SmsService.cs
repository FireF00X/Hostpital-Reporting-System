using HospitalReporting.Application.Services.ServicesInterfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace HospitalReporting.Application.Services.ServicesImplementation
{
    public class SmsService(IConfiguration _config) : ISmsService
    {
        public async Task<MessageResource> SendMessage(string phoneNumber, string bodyMessage)
        {
            var accountSid = _config["Twilio:AccountSID"];
            var authToken = _config["Twilio:AuthToken"];

            TwilioClient.Init(accountSid, authToken);

            var message = await MessageResource.CreateAsync(
            body: bodyMessage,
            from: _config["Twilio:TwilioPhoneNumber"],
            to: phoneNumber);

            return message;
        }
    }
}
