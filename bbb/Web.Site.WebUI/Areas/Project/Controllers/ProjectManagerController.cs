using Core.Service;
using Domain.Site.Models;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web.Site.WebUI.Areas.Project.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("ProjectManager")]
    [RouteArea("Project")]
    public class ProjectManagerController : Controller
    {
        #region 属性
        [Import]
        private IProjectManager projectService;

        #endregion
        // GET: Project/Project
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProjectProfile(string projectId)
        {
            return View();
        }

        public ActionResult ProjectList(ProjectSerchModel query)
        {
            return Json( projectService.GetCurrentUserProjectViewModel(query), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateProject(ProjectViewModel model)
        {
            var result = checkProjectViewModel(model);
            if(result.IsSuccess)
            {
                result.Result = projectService.CreateProject(model);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private ActionResultViewModel checkProjectViewModel(ProjectViewModel model)
        {
            var result = new ActionResultViewModel() {IsSuccess=true };
            if (model == null)
            {
                result.IsSuccess = false;
                result.Result = "项目信息为空";
                return result;
            }
            if (model.ProjectName == null)
            {
                result.IsSuccess = false;
                result.Result = "项目名称为空";
                return result;
            }
            if (model.LeaderName == null)
            {
                result.IsSuccess = false;
                result.Result = "负责人为空";
            }
            return result;
        }


    }
}