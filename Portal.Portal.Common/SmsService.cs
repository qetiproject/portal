using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Portal.Portal.Common
{
    public class SmsService
    {
        private readonly string accountSid = "AC05b35386e8c3e4e9e31ac989aa2d9aef";
        private readonly string authToken = "e9a93de47d57ed3c02494a9a65e0a158";
        private readonly string senderPhoneNumber = "+13158175251";

        public void SendSms(string recipientPhoneNumber, string message)
        {
            TwilioClient.Init(accountSid, authToken);

            var smsMessage = MessageResource.Create(
                body: message,
                from: new Twilio.Types.PhoneNumber(senderPhoneNumber),
                to: new Twilio.Types.PhoneNumber(recipientPhoneNumber)
            );

            Console.WriteLine(smsMessage.Sid); // Optionally, you can log the SMS SID for reference
        }
    }
}
