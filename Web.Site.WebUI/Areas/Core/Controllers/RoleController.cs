using Core.Service;
using Domain.Site.Models.Core;
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
    public class RoleController : Controller
    {
        [Import]
        private IRoleService _roleService { get; set; }
        // GET: System/Role
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetMenuByRoleId(int roleId)
        {
            return Json(_roleService.GetMenuByRoleId(roleId));
        }

        [HttpGet]
        public ActionResult GetAllRole()
        {
            return Json(_roleService.GetAllRoleKeyValue(),JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetRoleList(RoleQueryModel query)
        {
            return Json(_roleService.GetRoleListByQuery(query), JsonRequestBehavior.AllowGet);
        }


    }
}