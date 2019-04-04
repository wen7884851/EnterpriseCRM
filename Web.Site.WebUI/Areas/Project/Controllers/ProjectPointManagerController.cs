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

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
         public ActionResult GetProjectPointListByProjectId(int projectId)
        {
            return Json(_projectPointManager.GetProjectPointListByProjectId(projectId));
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
    }
}