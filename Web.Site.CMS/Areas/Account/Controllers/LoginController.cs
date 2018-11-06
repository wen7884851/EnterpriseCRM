using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace Web.Site.CMS.Areas.Account.Controllers
{
    public class LoginController : Controller
    {
        // GET: Account/Login
        public ActionResult Index()
        {
            return View();
        }
    }
}