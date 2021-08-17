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
                {"2", "+19362515374"},
                {"3", "+19362514886"},
                {"4", "+15755672172"}
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
