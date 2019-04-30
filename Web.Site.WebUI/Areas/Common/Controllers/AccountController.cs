﻿using System;
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

        public ActionResult index()
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
            return RedirectToAction("Login");
        }
        [HttpPost]
        public ActionResult ChangePwd(UserAccountViewModel model)
        {
            model.UserId = OperatorProvider.Provider.GetCurrent().UserId;
            return Json(_userService.ChangeUserPassWord(model));
        }

        [HttpPost]
        public ActionResult GetUserList()
        {
            var userList = _userService.GetAllUser().Select(t => new OptionViewMode
            {
                key = t.UserId,
                text = t.LoginName+"("+t.FullName+")",
                value = t.UserId
            }).ToList();
            return Json(userList, JsonRequestBehavior.AllowGet);
        }
    }
}