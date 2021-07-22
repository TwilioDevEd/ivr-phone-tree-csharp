using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Core;
using Twilio.AspNet.Mvc;
using Twilio.TwiML;
using Twilio.TwiML.Voice;

namespace IVRPhoneTree.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IVRController : TwilioController
    {
        // POST: IVR/Welcome
        [HttpPost("Welcome")]
        public TwiMLResult Welcome()
        {
            var response = new VoiceResponse();
            var gather = new Gather(action: Url.ActionUri("Show", "Menu"), numDigits: 1);
            gather.Say("Thank you for calling the E.T. Phone Home Service - the " +
                       "adventurous alien's first choice in intergalactic travel. " +
                       "Press 1 for directions, press 2 to make a call.");
            response.Append(gather);

            return TwiML(response);
        }
    }
}
