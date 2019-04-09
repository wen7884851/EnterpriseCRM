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
using System.Threading.Tasks;

namespace Web.Site.WebUI.Areas.Common.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountController : Controller
    {
        //
        // GET: /Admin/Account/

        #region 属性
        [Import]
        private IUserService _userService { get; set; }

        #endregion

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckLogin(UserAccountViewModel model)
        {
            var result = _userService.CheckLogin(model);
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

        [HttpPost]
        public ActionResult GetUserList()
        {
            var userList = _userService.GetAllUser().Select(t => new OptionViewMode
            {
                key = t.Id,
                text = t.LoginName,
                value = t.Id
            }).ToList();
            return Json(userList, JsonRequestBehavior.AllowGet);
        }
    }
}