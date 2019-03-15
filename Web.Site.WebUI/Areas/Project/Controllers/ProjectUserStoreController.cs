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
    public class ProjectUserStoreController : Controller
    {
        [Import]
        private IProjectUserStoreManager _projectUserStoreManager;
        // GET: Project/ProjectUserStore
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetUserStoreById(int storeId)
        {
            var store = _projectUserStoreManager.projectPointUserStores.FirstOrDefault(t => t.Id == storeId);
            return Json(store);
        }

        [HttpPost]
        public ActionResult CreateUserStore(ProjectUserStoreViewModel store)
        {
            return Json(_projectUserStoreManager.CreateProjectUserStore(store));
        }

        [HttpPost]
        public ActionResult UpdateUserStore(ProjectUserStoreViewModel store)
        {
            return Json(_projectUserStoreManager.UpdateProjectUserStore(store));
        }

        [HttpPost]
        public ActionResult GetUserStoreListByQuery(ProjectUserStoreQueryModel query)
        {
            return Json(_projectUserStoreManager.GetUserStoreListByQuery(query));
        }
    }
}