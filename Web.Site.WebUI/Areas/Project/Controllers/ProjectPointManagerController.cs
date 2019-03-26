﻿using Core.Service;
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
        private IProjectUserStoreManager _projectUserStoreManager;
        [Import]
        private IUserService _userService;

        [HttpPost]
        public ActionResult GetUserIdAndExceptPointUser(int pointId)
        {
           var userExcept = _projectUserStoreManager.GetUserStoreUserIdsByPointId(pointId);
           var userResult = _userService.Users
           .Select(t => new OptionViewMode
           {
               key = t.Id,
               text = t.LoginName,
               value = t.Id
           });
            if(userExcept!=null)
            {
                userResult = userResult.Where(t => !userExcept.Contains(t.key));
            }
            return Json(userResult.ToList());
        }

        [HttpPost]
        public ActionResult CreateProjectPoint(ProjectPointViewModel model)
        {
            return Json(_projectPointManager.CreateProjectPoint(model));
        }

        [HttpPost]
        public ActionResult UpdateProjectPoint(ProjectPointViewModel model)
        {
            return Json(_projectPointManager.UpdateProjectPoint(model));
        }

        [HttpPost]
        public ActionResult GetPointSurplusFund(int pointId)
        {
            return Json(_projectPointManager.getPointSurplusMoney(pointId));
        }
    }
}