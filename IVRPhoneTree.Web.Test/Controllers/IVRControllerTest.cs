using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using IVRPhoneTree.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace IVRPhoneTree.Web.Test.Controllers
{
    class IVRControllerTest
    {
        private StringBuilder _result;
        private Mock<ControllerContext> _mockControllerContext;

        [SetUp]
        public void Setup()
        {
            _result = new StringBuilder();
            var mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(s => s.Write(It.IsAny<string>())).Callback<string>(c => _result.Append(c));
            mockResponse.Setup(s => s.Output).Returns(new StringWriter(_result));

            _mockControllerContext = new Mock<ControllerContext>();
            _mockControllerContext.Setup(x => x.HttpContext.Response).Returns(mockResponse.Object);
        }

        [Test]
        public void Welcome_gather_digit_and_play_welcome_message()
        {
            var controller = new IVRController();
            var result = controller.Welcome();

            result.ExecuteResult(_mockControllerContext.Object);

            Assert.That(_result.ToString(), Is.EqualTo(
                "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n" +
                "<Response>\r\n" +
                "  <Gather action=\"/ivr/menu\" numDigits=\"1\">\r\n" +
                "    <Play loop=\"3\">http://howtodocs.s3.amazonaws.com/et-phone.mp3</Play>\r\n" +
                "  </Gather>\r\n" +
                "</Response>"
                ));
        }

        [Test]
        public void MenuSelection_say_about_extraction_and_hangup_point_when_1_is_pressed()
        {
            var controller = new IVRController();
            var result = controller.MenuSelection("1");

            result.ExecuteResult(_mockControllerContext.Object);

            Assert.That(_result.ToString(), Is.EqualTo(
                "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n" +
                "<Response>\r\n" +
                "  <Say voice=\"alice\" language=\"en-GB\">To get to your extraction point, get on your bike and go down the street. Then Left down an alley. Avoid the police cars. Turn left into an unfinished housing development. Fly over the roadblock. Go passed the moon. Soon after you will see your mother ship. </Say>\r\n" +
                "  <Say>Thank you for calling the ET Phone Home Service - the adventurous alien's first choice in intergalactic travel.</Say>\r\n" +
                "  <Hangup />\r\n" +
                "</Response>"
                ));
        }

        [Test]
        public void MenuSelection_gather_digit_and_say_message_when_2_is_pressed()
        {
            var controller = new IVRController();
            var result = controller.MenuSelection("2");

            result.ExecuteResult(_mockControllerContext.Object);

            Assert.That(_result.ToString(), Is.EqualTo(
                "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n" +
                "<Response>\r\n" +
                "  <Gather action=\"/ivr/planets\" numDigits=\"1\">\r\n" +
                "    <Say voice=\"alice\" language=\"en-GB\" loop=\"3\">To call the planet Broh doe As O G, press 2. To call the planet DuhGo bah, press 3. To call an oober asteroid to your location, press 4. To go back to the main menu, press the star key </Say>\r\n" +
                "  </Gather>\r\n" +
                "</Response>"
                ));
        }

        [Test]
        public void MenuSelection_say_returning_to_main_menu_and_redirect_when_other_number_is_pressed()
        {
            var controller = new IVRController();
            var result = controller.MenuSelection("3");

            result.ExecuteResult(_mockControllerContext.Object);

            Assert.That(_result.ToString(), Is.EqualTo(
                "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n" +
                "<Response>\r\n" +
                "  <Say voice=\"alice\" language=\"en-GB\">Returning to the main menu.</Say>\r\n" +
                "  <Redirect>/ivr/welcome</Redirect>\r\n" +
                "</Response>"
                ));
        }

        [TestCase("2", "Dial")]
        [TestCase("3", "Dial")]
        [TestCase("4", "Dial")]
        [TestCase("1", "Say")]
        public void PlanetSelection(string digits, string expectedVerb)
        {
            var controller = new IVRController();
            var result = controller.PlanetSelection(digits);

            result.ExecuteResult(_mockControllerContext.Object);

            StringAssert.Contains(expectedVerb, _result.ToString());
        }
    }
}
