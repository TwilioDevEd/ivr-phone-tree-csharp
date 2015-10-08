using System.Collections.Generic;
using System.Web.Mvc;
using Twilio.TwiML;
using Twilio.TwiML.Mvc;

namespace IVRPhoneTree.Web.Controllers
{
    public class PhoneExchangeController : ControllerBase
    {
        // POST: PhoneExchange/Interconnect
        [HttpPost]
        public TwiMLResult Interconnect(string digits)
        {
            var userOption = digits;
            var optionPhones = new Dictionary<string, string>
            {
                {"2", "+12024173378"},
                {"3", "+12027336386"},
                {"4", "+12027336637"}
            };

            return optionPhones.ContainsKey(userOption)
                ? Dial(optionPhones[userOption]) : RedirectWelcome();
        }

        private static TwiMLResult Dial(string phoneNumber)
        {
            var response = new TwilioResponse();
            response.Dial(phoneNumber);

            return new TwiMLResult(response);
        }
    }
}
