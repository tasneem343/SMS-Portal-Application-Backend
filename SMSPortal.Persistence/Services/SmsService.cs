using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using SMSPortal.Application.Helper;
using Twilio;
using Microsoft.Extensions.Options;
using Twilio.Rest.Api.V2010.Account;
using SMSPortal.Application.Services.SmsService;

namespace SMSPortal.Persistence.Services
{
    public class SMSService : ISmsService
    {
        private readonly TwilioSettings _twilio;

        public SMSService(IOptions<TwilioSettings> twilio)
        {
            _twilio = twilio.Value;
        }

        public async Task< MessageResource> Send(string mobileNumber, string body)
        {
            TwilioClient.Init(_twilio.AccountSID, _twilio.AuthToken);

            var result =await MessageResource.CreateAsync(
                    body: body,
                    from: new Twilio.Types.PhoneNumber(_twilio.TwilioPhoneNumber),
                    to: mobileNumber
                );

            return result;
        }

    }
}
