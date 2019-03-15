using Core.Service;
using Core.Service.Authen;
using Domain.Site.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Site.WebUI.Areas.Project.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectPointManagerController : Controller
    {
        [Import]
        private IProjectPointManager _projectPointManager;

        [Import]
        private IUserService _userService;

        [HttpPost]
        public ActionResult GetUserIdAndExceptPointUser(int pointId)
        {
           var userExcept = _projectPointManager.GetPointUserId(pointId);
           var userResult = _userService.Users.Where(t=>!userExcept.Contains(t.Id))
           .Select(t => new OptionViewMode
           {
               key = t.Id,
               text = t.LoginName,
               value = t.Id
           }).ToList();
            return Json(userResult);
        }

        [HttpPost]
        public ActionResult GetPointSurplusFund(int pointId)
        {
            return Json(_projectPointManager.getPointSurplusMoney(pointId));
        }
    }
}