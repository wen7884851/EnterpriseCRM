using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;

using Framework.Tool;
using Core.Service.Authen;
using Domain.Site.Models;
using Framework.Common.SecurityHelper;
using Framework.Common.ToolsHelper.Net;
using System.Configuration;
using Newtonsoft.Json;
using Framework.Tool.Operator;

namespace Web.Site.WebUI.Areas.Common.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoginController : Controller
    {
        //
        // GET: /Common/Login/

        #region 属性
        [Import]
        public IUserService UserService { get; set; }

        //[Import]
        //public ILoginLogService LoginLogService { get; set; }

        #endregion

        public ActionResult Index()
        {
            return View();

        }
        //public ActionResult Index(string token)
        //{
        //    // 1、调用apitool  传参：username，systemgid，apiurl
        //    return View();
        //}
        [HttpPost]
        public ActionResult CheckLogin(UserAccountModel model)
        {
            var result = UserService.CheckLogin(model);
            return Json(result);
        }

        public ActionResult SignOut()
        {
           

            OperatorProvider.Provider.RemoveCurrent();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ForgetPwd()
        {
            return PartialView();
        }
    }
}