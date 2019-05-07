using Core.Service;
using Domain.Site.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Site.WebUI.Areas.Core.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ModuleController : Controller
    {
        [Import]
        private IModuleService _moduleService { get; set; }
        // GET: Core/Module
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetModuleListByQuery(ModuleQueryModel query)
        {
            return Json(_moduleService.GetModuleListByQuery(query));
        }

        [HttpPost]
        public ActionResult GetAllModule()
        {
            return Json(_moduleService.GetAllModuleList());
        }

        [HttpPost]
        public ActionResult GetLayerKeyValue()
        {
            return Json(_moduleService.GetLayerKeyValue());
        }
        [HttpPost]
        public ActionResult GetParentModuleKeyValue(int layer)
        {
            return Json(_moduleService.GetParentModuleKeyValue(layer));
        }

        [HttpPost]
        public ActionResult CreateModule(ModuleViewModel model)
        {
            return Json(_moduleService.CreateModule(model));
        }
    }
}