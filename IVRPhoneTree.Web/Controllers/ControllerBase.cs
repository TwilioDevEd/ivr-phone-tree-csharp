using Twilio.TwiML;
using Twilio.TwiML.Mvc;

namespace IVRPhoneTree.Web.Controllers
{
    public abstract class ControllerBase : TwilioController
    {
        public TwiMLResult RedirectWelcome()
        {
            var response = new TwilioResponse();
            response.Say("Returning to the main menu",
                new {voice = "alice", language = "en-GB"});
            response.Redirect(Url.Action("Welcome", "IVR"));

            return new TwiMLResult(response);
        }
    }
}