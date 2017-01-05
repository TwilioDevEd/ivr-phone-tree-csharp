using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Twilio.TwiML;

namespace IVRPhoneTree.Web.Controllers
{
    public class MenuController : ControllerBase
    {
        // POST: Menu/Show
        [HttpPost]
        public ActionResult Show(string digits)
        {
            var selectedOption = digits;
            var optionActions = new Dictionary<string, Func<ActionResult>>()
            {
                {"1", ReturnInstructions},
                {"2", Planets}
            };

            return optionActions.ContainsKey(selectedOption) ?
                optionActions[selectedOption]() :
                RedirectWelcome();
        }

        private ActionResult ReturnInstructions()
        {
            var response = new VoiceResponse();
            response.Say("To get to your extraction point, get on your bike and go down " +
                         "the street. Then Left down an alley. Avoid the police cars. Turn left " +
                         "into an unfinished housing development. Fly over the roadblock. Go " +
                         "passed the moon. Soon after you will see your mother ship.",
                         voice: "alice", language: "en-GB");

            response.Say("Thank you for calling the ET Phone Home Service - the " +
                         "adventurous alien's first choice in intergalactic travel");

            response.Hangup();

            return Content(response.ToString(), "application/xml");
        }

        private ActionResult Planets()
        {
            var response = new VoiceResponse();
            var gather = new Gather(action: Url.Action("Interconnect", "PhoneExchange"), numDigits: 1);
            gather.Say("To call the planet Broh doe As O G, press 2. To call the planet " +
                     "DuhGo bah, press 3. To call an oober asteroid to your location, press 4. To " +
                     "go back to the main menu, press the star key ",
                     voice: "alice", language: "en-GB", loop: 3);
            response.Gather(gather);

            return Content(response.ToString(), "application/xml");
        }
    }
}