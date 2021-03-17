using System;
using System.Collections.Generic;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Market.Verification
{
    public class TwilioClass : IVerification
    {
        public string SendMessage(string phoneNumber)
        {
            string accountSid = "AC6c82d03b79d8909634380cb011b9c6cd";
            string authToken = "57d4545f9bf91dc42b9d40226c7ddc33";


            Random random = new Random();
            string code = random.Next(100000, 999999).ToString(); ;

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: $"Ваш код для регистрации: {code}",
                from: new global::Twilio.Types.PhoneNumber("+12059472605"),
                to: new global::Twilio.Types.PhoneNumber(phoneNumber)
            );

            return code;
        }


        public static bool CheckCode(string firstCode, string secondCode)
        {
            if (firstCode == secondCode) return true;
            else return false;
        }

    }
}
