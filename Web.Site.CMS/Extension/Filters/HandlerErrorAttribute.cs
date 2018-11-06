using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Core.Log;
using Core.Common.ToolsHelper.Net;
using Core.Tool.Operator;
using Core.Tool;
using Core.Common.ToolsHelper;
using Core.Common.JsonHelper;

namespace Web.Site.CMS.Extension.Filter
{
    /// <summary>
    ///AllowMultiple:false 一个控制器只执行一次
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class HandlerErrorAttribute : HandleErrorAttribute
    {
      //  private ILogService LogService { get; set; }

        public HandlerErrorAttribute()
        {
            var container = new HttpContextHelp().HttpContextGet("Container");//HttpContext.Current.Application["Container"] as CompositionContainer;
          //  LogService = container.GetExportedValueOrDefault<ILogService>();
        }
        public override void OnException(ExceptionContext context)
        {
            #region 获取信息
            LogMessage logMessage = new LogMessage();
            var log = LogFactory.GetLogger(context.Controller.ToString());
            Exception Error = context.Exception;

            logMessage.OperationTime = DateTime.Now;
            logMessage.Url = HttpContext.Current.Request.RawUrl;
            logMessage.Class = context.Controller.ToString();
            logMessage.Ip = Net.Ip;
            logMessage.Host = Net.Host;
            logMessage.Browser = Net.Browser;
            logMessage.ExceptionInfo = context.Exception.Message;
            logMessage.UserName = OperatorProvider.Provider.GetCurrent()?.LoginName + "（" + OperatorProvider.Provider.GetCurrent()?.FullName + "）";
            logMessage.ExceptionSource = Error.Source;
            logMessage.ExceptionRemark = Error.StackTrace;
            string strMessage = new LogFormat().ExceptionFormat(logMessage);
            SendMail(strMessage);
            #endregion

            //LogService.WriteLog(context);
            Exception exception = context.Exception;
            if (context.HttpContext.Request.IsAjaxRequest())//检查请求头
            {
                var message = "Ajax请求异常：";
                if (exception is HttpAntiForgeryException)
                {
                    message += "安全性验证失败。<br>请刷新页面重试，详情请查看系统日志。";
                }
                else
                {
                    message += exception.Message;
                }
                context.Result = new ContentResult { Content = new OperationResult { ResultType = OperationResultType.Error, Message = message }.ToJson() };
                context.ExceptionHandled = true;
            }
            else
            {
                base.OnException(context);
                context.ExceptionHandled = true;
                context.HttpContext.Response.StatusCode = 200;
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Common", controller = "Error", action = "ErrorMessage", message = context.Exception.Message }));
            }


        }
     

        /// <summary>
        /// 发送邮件
        /// </summary>
        private void SendMail(string body)
        {
            bool ErrorToMail = Configs.GetValue("ErrorToMail").ObjToBool();
            string SystemName = Configs.GetValue("SystemName");//系统名称
            if (ErrorToMail)
            {
                MailHelper.Send(Configs.GetValue("MailUserName") , SystemName + " - 发生异常", body.Replace("-", ""));
            }
        }
    }
}