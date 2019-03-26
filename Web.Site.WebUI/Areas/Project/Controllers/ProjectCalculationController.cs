using Core.Service;
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
    public class ProjectCalculationController : Controller
    {
        #region 属性
        [Import]
        private IProjectCalculationFormula _projectCalculationFormula;

        #endregion

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetProjectType()
        {
            var projectType = _projectCalculationFormula.projectTypes.Select(t => new OptionViewMode
            {
                key = t.Id,
                text = t.TypeName,
                value = t.Id
            }).ToList();
            return Json(projectType);
        }

        [HttpPost]
        public ActionResult CalculationProjectCommission(ProjectCalculationViewModel model)
        {
            return Json(_projectCalculationFormula.CommonCalculationCommission(model), JsonRequestBehavior.AllowGet);
        }
    }
}