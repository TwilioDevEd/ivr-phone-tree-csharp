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
    public class PhoneExchangeControllerTest
    {
        private PhoneExchangeController _controller;

        [SetUp]
        public void SetUp()
        {
            var controllerPropertiesMock = new ControllerPropertiesMock();
            _controller = new PhoneExchangeController
            {
                ControllerContext = controllerPropertiesMock.ControllerContext,
                Url = controllerPropertiesMock.Url(RouteConfig.RegisterRoutes)
            };
        }

        [TestCase("2")]
        [TestCase("3")]
        [TestCase("4")]
        public void GivenAShowAction_WhenTheSelectedOptionIsEither_2_Or_3_Or_4_ThenTheResponseContainsDial(string userOption)
        {
            _controller.WithCallTo(c => c.Interconnect(userOption))
                .ShouldReturnTwiMLResult(data =>
                {
                    Assert.That(data.XPathSelectElement("Response/Dial"), Is.Not.Null);
                    Assert.That(data.XPathSelectElement("Response/Dial").Value, Is.Not.Null);
                    Assert.That(data.XPathSelectElement("Response/Redirect"), Is.Null);
                });
        }

        [Test]
        public void GivenAShowAction_WhenTheSelectedOptionIsDifferentThan_2_Or_3_Or_4_ThenTheResponseRedirectsToIVRWelcome()
        {
            _controller.WithCallTo(c => c.Interconnect("*"))
                .ShouldReturnTwiMLResult(data =>
                {
                    Assert.That(data.XPathSelectElement("Response/Redirect").Value, Is.EqualTo("/IVR/Welcome"));
                });
        }
    }
}
