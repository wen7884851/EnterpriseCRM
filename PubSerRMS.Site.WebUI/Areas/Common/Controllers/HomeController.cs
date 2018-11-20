using Domain.Site.Common.Models;
using Domain.Site.WebUI.Common;
using Domain.Site.WebUI.Extension.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Domain.Site.WebUI.Areas.Common.Controllers
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