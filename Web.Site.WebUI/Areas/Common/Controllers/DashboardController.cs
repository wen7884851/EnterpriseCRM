using Core.Service.Authen;
using Framework.Tool.Operator;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Web.Site.WebUI.Common;

namespace Web.Site.WebUI.Areas.Common.Controllers
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DashboardController : AdminController
    {
        [Import]
        private IUserService _userService { get; set; }
        //
        // GET: /Admin/Dashboard/
        public ActionResult Index()
        {
            var user = OperatorProvider.Provider.GetCurrent();
            if(user.IsFirstLogin)
            {
                return RedirectToAction("SetUserProfile");
            }
            else
            {
                return View();
            }
        }

        public ActionResult SetUserProfile()
        {
            ViewBag.User= OperatorProvider.Provider.GetCurrent().UserId;
            return View();
        }

    }
}