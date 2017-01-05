using System.Web.Mvc;
using Twilio.TwiML;

namespace IVRPhoneTree.Web.Controllers
{
    public class IVRController : Controller
    {
        // GET: IVR
        public ActionResult Index()
        {
            return View();
        }

        // POST: IVR/Welcome
        [HttpPost]
        public ActionResult Welcome()
        {
            var response = new VoiceResponse();
            var gather = new Gather(action: Url.Action("Show", "Menu"), numDigits: 1);
            gather.Play("http://howtodocs.s3.amazonaws.com/et-phone.mp3", loop: 3);
            response.Gather(gather);

            return Content(response.ToString(), "application/xml");
        }
    }
}