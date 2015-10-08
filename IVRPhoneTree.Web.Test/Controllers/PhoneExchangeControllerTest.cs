using IVRPhoneTree.Web.Controllers;
using NUnit.Framework;

// ReSharper disable PossibleNullReferenceException

namespace IVRPhoneTree.Web.Test.Controllers
{
    public class PhoneExchangeControllerTest : ControllerTest
    {
        [TestCase("2")]
        [TestCase("3")]
        [TestCase("4")]
        public void GivenAShowAction_WhenTheSelectedOptionIsEither_2_Or_3_Or_4_ThenTheResponseContainsDial(string userOption)
        {
            var controller = new PhoneExchangeController {Url = Url};
            var result = controller.Interconnect(userOption);

            result.ExecuteResult(MockControllerContext.Object);

            var document = LoadXml(Result.ToString());

            Assert.That(document.SelectSingleNode("Response/Dial"), Is.Not.Null);
            Assert.That(document.SelectSingleNode("Response/Dial").InnerText, Is.Not.Null);
            Assert.That(document.SelectSingleNode("Response/Redirect"), Is.Null);
        }

        [Test]
        public void GivenAShowAction_WhenTheSelectedOptionIsDifferentThan_2_Or_3_Or_4_ThenTheResponseRedirectsToIVRWelcome()
        {
            var controller = new PhoneExchangeController { Url = Url };
            var result = controller.Interconnect("*");

            result.ExecuteResult(MockControllerContext.Object);

            var document = LoadXml(Result.ToString());

            Assert.That(document.SelectSingleNode("Response/Redirect").InnerText, Is.EqualTo("/IVR/Welcome"));
        }
    }
}
