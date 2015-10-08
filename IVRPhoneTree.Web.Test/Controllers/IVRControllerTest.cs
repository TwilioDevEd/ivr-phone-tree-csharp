using IVRPhoneTree.Web.Controllers;
using NUnit.Framework;

// ReSharper disable PossibleNullReferenceException

namespace IVRPhoneTree.Web.Test.Controllers
{
    public class IVRControllerTest : ControllerTest
    {

        [Test]
        public void GivenAWelcomeAction_ThenTheResponseContainsGatherPlay()
        {
            var controller = new IVRController { Url = Url };
            var result = controller.Welcome();

            result.ExecuteResult(MockControllerContext.Object);

            var document = LoadXml(Result.ToString());

            Assert.That(document.SelectSingleNode("Response/Gather/Play"), Is.Not.Null);
            Assert.That(document.SelectSingleNode("Response/Gather").Attributes["action"].Value,
                Is.EqualTo("/Menu/Show"));
        }
    }
}