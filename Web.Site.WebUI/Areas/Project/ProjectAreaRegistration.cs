using System.Web.Mvc;

namespace Web.Site.WebUI.Areas.Project
{
    public class ProjectAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Project";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Project_default",
                "Project/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Web.Site.WebUI.Areas.Project.Controllers" }
            );
        }
    }
}