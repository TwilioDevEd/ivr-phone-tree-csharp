using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml;
using Moq;
using NUnit.Framework;

namespace IVRPhoneTree.Web.Test.Controllers
{
    public abstract class ControllerTest
    {
        protected StringBuilder Result;
        protected Mock<ControllerContext> MockControllerContext;

        [SetUp]
        public void Setup()
        {
            Result = new StringBuilder();
            var mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(s => s.Write(It.IsAny<string>())).Callback<string>(c => Result.Append(c));
            mockResponse.Setup(s => s.Output).Returns(new StringWriter(Result));

            MockControllerContext = new Mock<ControllerContext>();
            MockControllerContext.Setup(x => x.HttpContext.Response).Returns(mockResponse.Object);
        }

        protected UrlHelper Url
        {
            get
            {
                var controllerName = GetType().Name.Replace("ControllerTest", string.Empty);
                var routes = new RouteCollection();
                routes.MapRoute(
                    "Default",
                    "{controller}/{action}/{id}",
                    new { controller = controllerName, action = "action", id = UrlParameter.Optional }
                );

                var httpContextMock = new Mock<HttpContextBase>();
                httpContextMock.Setup(c => c.Request.ApplicationPath).Returns("/");
                httpContextMock.Setup(c => c.Response.ApplyAppPathModifier(It.IsAny<string>()))
                   .Returns<string>(s => s);

                var routeData = new RouteData();
                var requestContext = new RequestContext(httpContextMock.Object, routeData);

                return new UrlHelper(requestContext, routes);
            }
        }

        protected XmlDocument LoadXml(string xml)
        {
            var document = new XmlDocument();
            document.LoadXml(xml);

            return document;
        }
    }
}