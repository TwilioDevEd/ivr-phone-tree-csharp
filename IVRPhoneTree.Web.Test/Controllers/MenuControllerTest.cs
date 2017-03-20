using System.Linq;
using System.Xml.XPath;
using FluentMvcTesting.Extensions;
using FluentMvcTesting.Extensions.Mocks;
using IVRPhoneTree.Web.Controllers;
using IVRPhoneTree.Web.Test.Extensions;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

// ReSharper disable PossibleNullReferenceException

namespace IVRPhoneTree.Web.Test.Controllers
{
    public class MenuControllerTest
    {
        private MenuController _controller;

        [SetUp]
        public void SetUp()
        {
            var controllerPropertiesMock = new ControllerPropertiesMock();
            _controller = new MenuController
            {
                ControllerContext = controllerPropertiesMock.ControllerContext,
                Url = controllerPropertiesMock.Url(RouteConfig.RegisterRoutes)
            };
        }

        [Test]
        public void GivenAShowAction_WhenTheSelectedOptionIs1_ThenTheResponseContainsSayTwiceAndAHangup()
        {
            _controller.WithCallTo(c => c.Show("1"))
                .ShouldReturnTwiMLResult(data =>
                {
                    Assert.That(data.XPathSelectElements("Response/Say").Count(), Is.EqualTo(2));
                    Assert.That(data.XPathSelectElement("Response/Hangup"), Is.Not.Null);
                });
        }

        [Test]
        public void GivenAShowAction_WhenTheSelectedOptionIs2_ThenTheResponseContainsGatherAndSay()
        {
            _controller.WithCallTo(c => c.Show("2"))
                .ShouldReturnTwiMLResult(data =>
                {
                    Assert.That(data.XPathSelectElement("Response/Gather/Say"), Is.Not.Null);
                    Assert.That(data.XPathSelectElement("Response/Gather").Attribute("action").Value,
                        Is.EqualTo("/PhoneExchange/Interconnect"));
                });
        }

        [Test]
        public void GivenAShowAction_WhenTheSelectedOptionIsDifferentThan_1_Or_2_ThenTheResponseRedirectsToIVRWelcome()
        {
            _controller.WithCallTo(c => c.Show("*"))
                .ShouldReturnTwiMLResult(data =>
                {
                    Assert.That(data.XPathSelectElement("Response/Redirect").Value,
                        Is.EqualTo("/IVR/Welcome"));
                });
        }
    }
}
