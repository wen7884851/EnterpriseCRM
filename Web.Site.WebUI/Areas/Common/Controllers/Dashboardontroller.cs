using System.ComponentModel.Composition;
using System.Web.Mvc;
using Web.Site.WebUI.Common;

namespace Web.Site.WebUI.Areas.Common.Controllers
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class Dashboardontroller : AdminController
    {
        //
        // GET: /Admin/Dashboard/
        public ActionResult Index()
        {

            return View();
        }
	}
}