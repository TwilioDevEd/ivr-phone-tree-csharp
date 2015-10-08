using IVRPhoneTree.Web.Controllers;
using NUnit.Framework;

// ReSharper disable PossibleNullReferenceException

namespace IVRPhoneTree.Web.Test.Controllers
{
    public class MenuControllerTest : ControllerTest
    {
        [Test]
        public void GivenAShowAction_WhenTheSelectedOptionIs1_ThenTheResponseContainsSayTwiceAndAHangup()
        {
            var controller = new MenuController();
            var result = controller.Show("1");

            result.ExecuteResult(MockControllerContext.Object);

            var document = LoadXml(Result.ToString());

            Assert.That(document.SelectNodes("Response/Say").Count, Is.EqualTo(2));
            Assert.That(document.SelectSingleNode("Response/Hangup"), Is.Not.Null);
        }

        [Test]
        public void GivenAShowAction_WhenTheSelectedOptionIs2_ThenTheResponseContainsGatherAndSay()
        {
            var controller = new MenuController {Url = Url};
            var result = controller.Show("2");

            result.ExecuteResult(MockControllerContext.Object);

            var document = LoadXml(Result.ToString());

            Assert.That(document.SelectSingleNode("Response/Gather/Say"), Is.Not.Null);
            Assert.That(document.SelectSingleNode("Response/Gather").Attributes["action"].Value,
                Is.EqualTo("/PhoneExchange/Interconnect"));
        }

        [Test]
        public void GivenAShowAction_WhenTheSelectedOptionIsDifferentThan_1_Or_2_ThenTheResponseRedirectsToIVRWelcome()
        {
            var controller = new MenuController { Url = Url };
            var result = controller.Show("*");

            result.ExecuteResult(MockControllerContext.Object);

            var document = LoadXml(Result.ToString());

            Assert.That(document.SelectSingleNode("Response/Redirect").InnerText, Is.EqualTo("/IVR/Welcome"));
        }
    }
}
