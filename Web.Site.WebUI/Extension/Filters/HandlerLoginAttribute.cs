
using Framework.Common.ToolsHelper;
using Framework.Tool.Operator;
using Domain.Site.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MC.WebSite.Extension.Filters
{
    /// <summary>
	///  描 述：登录认证（会话验证组件）
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class HandlerLoginAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        private LoginMode _customMode;
        public HandlerLoginAttribute(LoginMode Mode)
        {
            _customMode = Mode;
        }

        /// <summary>
        /// 响应前执行登录验证,查看当前用户是否有效 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //登录拦截是否忽略
            if (_customMode == LoginMode.Ignore)
            {
                return;
            }
            //登录是否过期
            if (OperatorProvider.Provider.IsOverdue())
            {
                CookieHelper.SetCookie("pubser_login_error", "Overdue");//登录已超时,请重新登录
                filterContext.Result = new RedirectResult("~/Admin/Login");
                return;
            }
            //是否已登录
            var OnLine = OperatorProvider.Provider.IsOnLine();
            if (OnLine == 0)
            {
                CookieHelper.SetCookie("pubser_login_error", "OnLine");//您的帐号已在其它地方登录,请重新登录
                filterContext.Result = new RedirectResult("~/Admin/Login");
                return;
            }
            else if (OnLine == -1)
            {
                CookieHelper.SetCookie("pubser_login_error", "-1");//缓存已超时,请重新登录
                filterContext.Result = new RedirectResult("~/Admin/Login");
                return;
            }

            //是否已登录子系统
           // var OnLineSS = OperatorProvider.Provider.IsOnLineSS();
            if (OnLine == 0)
            {
                CookieHelper.SetCookie("pubser_login_error", "OnLine");//您的帐号已在其它地方登录,请重新登录
                filterContext.Result = new RedirectResult("~/Admin/Login");
                return;
            }
            else if (OnLine == -1)
            {
                CookieHelper.SetCookie("pubser_login_error", "-1");//缓存已超时,请重新登录
                filterContext.Result = new RedirectResult("~/Admin/Login");
                return;
            }






        }



    }
}