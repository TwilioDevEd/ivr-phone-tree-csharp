using System.Xml.XPath;
using FluentMvcTesting.Extensions.Mocks;
using IVRPhoneTree.Web.Controllers;
using IVRPhoneTree.Web.Test.Extensions;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

// ReSharper disable PossibleNullReferenceException

namespace IVRPhoneTree.Web.Test.Controllers
{
    public class IVRControllerTest
    {
        [Test]
        public void GivenAWelcomeAction_ThenTheResponseContainsGatherPlay()
        {
            var controllerPropertiesMock = new ControllerPropertiesMock();
            var controller = new IVRController
            {
                ControllerContext = controllerPropertiesMock.ControllerContext,
                Url = controllerPropertiesMock.Url(RouteConfig.RegisterRoutes)
            };

            controller.WithCallTo(c => c.Welcome())
                .ShouldReturnTwiMLResult(data =>
                {
                    Assert.That(data.XPathSelectElement("Response/Gather/Say"), Is.Not.Null);
                    Assert.That(data.XPathSelectElement("Response/Gather").Attribute("action").Value,
                        Is.EqualTo("/Menu/Show"));
                });
        }
    }
}