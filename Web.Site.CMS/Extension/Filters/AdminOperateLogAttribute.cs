using System;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Site.CMS.Extension.Filters
{
    /// <summary>
    /// 记录动作Log
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AdminOperateLogAttribute : ActionFilterAttribute
    {
       // public IPermissionService PermissionService { get; set; }
      //  public IOperateLogService OperateLogService { get; set; }

        public AdminOperateLogAttribute()
        {
           // var container = new HttpContextHelp().HttpContextGet("Container");// HttpContext.Current.Application[] as CompositionContainer;
           // PermissionService = container.GetExportedValueOrDefault<IPermissionService>();
           // OperateLogService = container.GetExportedValueOrDefault<IOperateLogService>();
        }


        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //var user = OperatorProvider.Provider.GetCurrent();
            //if (user != null)
            //{
            //    var area = filterContext.RouteData.DataTokens["area"] != null ? filterContext.RouteData.DataTokens["area"].ToString() : "";
            //    var controller = filterContext.RouteData.Values["controller"].ToString();
            //    var action = filterContext.RouteData.Values["action"].ToString();
            //    var actionName = action.ToLower();
            //    var permission = PermissionService.Permissions.FirstOrDefault(t => t.Code.ToLower() == actionName && t.Enabled == true && t.IsDeleted == false);
            //    var description = string.Empty;
            //    if (permission != null)
            //    {
            //        description = permission.Name;
            //    }

            //    var model = new OperateLogModel
            //    {
            //        Area = area,
            //        Controller = controller,
            //        Action = action,
            //        Description = description,
            //        LogTime = DateTime.Now,
            //        UserId = user.UserId,
            //        LoginName = user.LoginName,
            //        IPAddress = Tools.GetUserIp()
            //    };

            //    OperateLogService.Insert(model);
            //}
        }
    }
}