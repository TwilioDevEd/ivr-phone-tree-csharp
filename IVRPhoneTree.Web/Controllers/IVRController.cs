using System;
using System.Web.Mvc;
using Twilio.TwiML;
using Twilio.TwiML.Mvc;

namespace IVRPhoneTree.Web.Controllers
{
    public class IVRController : TwilioController
    {
        public ActionResult Index()
        {
            return Content("Dial me");
        }

        public TwiMLResult Welcome()
        {
            var response = new TwilioResponse();
            response.BeginGather(new {action = "", numDigits = "1"})
                .Play("http://howtodocs.s3.amazonaws.com/et-phone.mp3", new {loop = 3})
                .EndGather();

            return TwiML(response);
        }

        [ActionName("Selection")]
        public TwiMLResult MenuSelection(int userSelection)
        {
            string message;
            TwilioResponse response;
            switch (userSelection)
            {
                case 1:
                    message = "To get to your extraction point, get on your bike and go down " +
                              "the street. Then Left down an alley. Avoid the police cars. Turn left " +
                              "into an unfinished housing development. Fly over the roadblock. Go " +
                              "passed the moon. Soon after you will see your mother ship. ";
                    
                    response = Say(message, true);
                    return TwiML(response);
                case 2:
                    return ListPlanets();
                default:
                    message = "Returning to the main menu.";

                    response = Say(message);
                    return TwiML(response);
            }
        }

        private TwiMLResult ListPlanets()
        {
            const string message = "To call the planet Broh doe As O G, press 2. To call the planet " +
                                   "DuhGo bah, press 3. To call an oober asteroid to your location, press 4. To " +
                                   "go back to the main menu, press the star key ";

            var response = new TwilioResponse();

            response.BeginGather(new {action = "", numDigits = "1"})
                .Say(message, new {voice = "alice", language = "en-GB", loop = "3"})
                .EndGather();

            return TwiML(response);
        }

        private static TwilioResponse Say(string message, bool exit = false)
        {
            var response = new TwilioResponse();
            response.Say(message, new {voice = "alice", language = "en-GB"});
            if (exit)
            {
                response.Say("Thank you for calling the ET Phone Home Service - the " +
                             "adventurous alien's first choice in intergalactic travel.");
                response.Hangup();
            }
            else
            {
                response.Redirect("irv/welcome");
            }

            return response;
        }
    }
}