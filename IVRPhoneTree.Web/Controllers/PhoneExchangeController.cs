using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace IVRPhoneTree.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhoneExchangeController : ControllerBase
    {
        // POST: PhoneExchange/Interconnect
        [HttpPost("Interconnect")]
        public TwiMLResult Interconnect(string digits)
        {
            var userOption = digits;
            var optionPhones = new Dictionary<string, string>
            {
                {"2", "+19295566487"},
                {"3", "+17262043675"},
                {"4", "+16513582243"}
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
