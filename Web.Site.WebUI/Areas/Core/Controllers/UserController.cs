using Core.Service.Authen;
using Domain.Site.Models;
using Framework.Common.FileHelper;
using Framework.Tool.Operator;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Web.Site.WebUI.Areas.Core.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserController : Controller
    {
        [Import]
        private IUserService _userService { get; set; }
        // GET: System/User
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadUserPhoto(UserPhotoViewModel model)
        {
            model.FileName= System.Guid.NewGuid() + "." + HttpContext.Request.Files["file"].FileName.Split('.')[1];
            model.FilePath = Request.PhysicalApplicationPath + "image\\main\\userProfile\\";
            model.File.SaveAs(model.FilePath+model.FileName);
            return Json(_userService.UploadUserPhoto(model));
        }

        [HttpPost]
        public ActionResult GetUserProfile(int userId)
        {
            return Json(_userService.GetUserProfileById(userId));
        }
        [HttpPost]
        public ActionResult GetUserAccountById(int userId)
        {
            return Json(_userService.GetUserAccountById(userId));
        }

        [HttpPost]
        public ActionResult EditUserProfile(UserProfileViewModel model)
        {
            return Json(_userService.UpdateUserProfile(model));
        }
        [HttpPost]
        public ActionResult GetUserListByQuery(UserSearchViewModel query)
        {
           return Json(_userService.GetUserListByQuery(query));
        }

        [HttpPost]
        public ActionResult CreateUser(UserAccountViewModel model)
        {
            return Json(_userService.CreateUser(model));
        }

        [HttpPost]
        public ActionResult ChangePwd(UserAccountViewModel model)
        {
            return Json(_userService.ChangeUserPassWordBySystemUser(model));
        }
        [HttpPost]
        public ActionResult DeleteUser(int userId)
        {
            return Json(_userService.DeleteUser(userId));
        }
    }
}