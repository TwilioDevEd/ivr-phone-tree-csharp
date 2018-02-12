using Twilio.AspNet.Mvc;
using Twilio.TwiML;

namespace IVRPhoneTree.Web.Controllers
{
    public abstract class ControllerBase : TwilioController
    {
        public TwiMLResult RedirectWelcome()
        {
            var response = new VoiceResponse();
            response.Say("Returning to the main menu",
                voice: "alice",
                language:  "en-GB"
            );
            response.Redirect(Url.ActionUri("Welcome", "IVR"));

            return TwiML(response);
        }
    }
}