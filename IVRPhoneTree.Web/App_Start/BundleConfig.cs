using System.Web.Optimization;

namespace IVRPhoneTree.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Scripts/lib/dist/css/font-awesome.css",
                      "~/Content/Site.css"));
        }
    }
}
