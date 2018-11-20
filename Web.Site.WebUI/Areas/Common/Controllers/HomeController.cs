using Domain.Site.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Site.WebUI.Common;
using Web.Site.WebUI.Extension.Filters;

namespace Web.Site.WebUI.Areas.Common.Controllers
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
	//[AdminPermission(PermissionCustomMode.Ignore)]
    public class HomeController : AdminController
    {
        //
        // GET: /Common/Home/

        [AdminLayout]
        public ActionResult Index()
        {

            return View();
        }
	}
}