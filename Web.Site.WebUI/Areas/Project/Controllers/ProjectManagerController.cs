using Core.Service;
using Domain.Site.Models;
using Framework.Tool.Operator;
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
        private IProjectManager _projectService;
        [Import]
        private IProjectPointManager _projectPointManager;

        #endregion
        // GET: Project/Project
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProjectProfile()
        {
            return View();
        }

        public ActionResult EditProjectPoint()
        {
            return View();
        }

        public ActionResult GetProjectPointById(int pointId)
        {
            return Json(_projectPointManager.GetPointById(pointId), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ProjectList(ProjectSerchModel query)
        {
            return Json( _projectService.GetCurrentUserProjectViewModel(query));
        }

        [HttpPost]
        public ActionResult GetProjectPointList(ProjectPointQueryModel queryModel)
        {
           // queryModel.UserId = 3;// OperatorProvider.Provider.GetCurrent().UserId;
            //如果是负责人这个UserId不需要赋值
            return Json(_projectPointManager.GetProjectPointListByQuery(queryModel));
        }

        [HttpPost]
        public ActionResult CreateProject(ProjectViewModel model)
        {
            var result = CheckProjectViewModel(model);
            if(result.IsSuccess)
            {
                result.Result = _projectService.CreateProject(model);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteProject(int projectId)
        {
            _projectService.DeleteProject(projectId);
            return Json(true);
        }

        [HttpPost]
        public ActionResult UpdateProject(ProjectViewModel model)
        {
            var result = CheckProjectViewModel(model);
            if (result.IsSuccess)
            {
                result.Result = _projectService.UpdateProject(model);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private ActionResultViewModel CheckProjectViewModel(ProjectViewModel model)
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
            if (model.ProjectLeader == null)
            {
                result.IsSuccess = false;
                result.Result = "负责人为空";
            }
            return result;
        }


    }
}