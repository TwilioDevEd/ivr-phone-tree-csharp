using System.Web.Mvc;
using Twilio.AspNet.Mvc;
using Twilio.TwiML;

namespace IVRPhoneTree.Web.Controllers
{
    public class IVRController : TwilioController
    {
        // GET: IVR
        public ActionResult Index()
        {
            return View();
        }

        // POST: IVR/Welcome
        [HttpPost]
        public TwiMLResult Welcome()
        {
            var response = new VoiceResponse();
            var gather = new Gather(action: Url.Action("Show", "Menu"), numDigits: 1);
            gather.Play("https://raw.githubusercontent.com/TwilioDevEd/ivr-phone-tree-servlets/master/et-phone.mp3", loop: 3);
            response.Gather(gather);

            return TwiML(response);
        }
    }
}
