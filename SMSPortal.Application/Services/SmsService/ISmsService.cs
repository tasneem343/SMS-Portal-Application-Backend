using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;

namespace SMSPortal.Application.Services.SmsService
{
    public interface ISmsService
    {
        public Task< MessageResource> Send(string mobileNumber, string body);
    }
}
