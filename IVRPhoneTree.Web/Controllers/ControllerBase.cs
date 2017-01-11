using System.Web.Mvc;
using Twilio.TwiML;

namespace IVRPhoneTree.Web.Controllers
{
    public abstract class ControllerBase : Controller
    {
        public ActionResult RedirectWelcome()
        {
            var response = new VoiceResponse();
            response.Say("Returning to the main menu",
                voice: "alice",
                language:  "en-GB"
            );
            response.Redirect(Url.Action("Welcome", "IVR"));

            return Content(response.ToString(), "text/xml");
        }
    }
}