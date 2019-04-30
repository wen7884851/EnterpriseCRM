using AutoMapper;
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
        private IProjectPointManager _projectPointManager { get; set; }
        [Import]
        private IUserService _userService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
         public ActionResult GetProjectPointListByProjectId(int projectId)
        {
            var pointList = _projectPointManager.GetProjectPointListByProjectId(projectId);
            return Json(pointList);
        }
        [HttpPost]
        public ActionResult GetPointById(int pointId)
        {
            return Json(_projectPointManager.GetPointById(pointId));
        }

        [HttpPost]
        public ActionResult GetExportCurrentPointUser(int pointId)
        {
            var point = _projectPointManager.projectPoints.FirstOrDefault(t => t.Id == pointId && t.IsDeleted == false);
            var allUser = _userService.GetAllUser();
            var exportPointUser = point.projectPointUserStores.Select(u => u.UserId.Value).ToList();
            var exportCurrentPointUser = allUser.Where(t => !exportPointUser.Contains(t.UserId))
                .Select(t => new OptionViewMode { value = t.UserId, text = t.LoginName + "(" + t.FullName + ")", key = t.UserId }).ToList();
            return Json(exportCurrentPointUser);
        }

        [HttpPost]
        public ActionResult GetProfessionalType()
        {
            return Json(_projectPointManager.pointProfessionalTypes.Where(t=>t.IsDeleted==false).Select(t=>new {Id=t.Id,TypeName=t.TypeName }));
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
        public ActionResult DeleteProjectPoint(int pointId)
        {
            return Json(_projectPointManager.DeleteProjectPoint(pointId));
        }
    }
}