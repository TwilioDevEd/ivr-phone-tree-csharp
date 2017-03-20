using System.Collections.Generic;
using System.Web.Mvc;
using Twilio.AspNet.Mvc;
using Twilio.TwiML;

namespace IVRPhoneTree.Web.Controllers
{
    public class PhoneExchangeController : ControllerBase
    {
        // POST: PhoneExchange/Interconnect
        [HttpPost]
        public ActionResult Interconnect(string digits)
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

        private TwiMLResult Dial(string phoneNumber)
        {
            var response = new VoiceResponse();
            response.Dial(phoneNumber);

            return TwiML(response);
        }
    }
}
