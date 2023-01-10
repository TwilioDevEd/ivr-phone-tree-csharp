using Microsoft.AspNetCore.Mvc;
using System;

namespace Twilio.AspNet.Mvc
{
    public static class TwilioUriHelper
    {
        public static Uri ActionUri(this IUrlHelper helper, string actionName, string controllerName)
        {
            // return new Uri(helper.Action(actionName, controllerName), UriKind.Relative);
            return new Uri(@$"/{controllerName}/{actionName}", UriKind.Relative);
        }
    }
}
