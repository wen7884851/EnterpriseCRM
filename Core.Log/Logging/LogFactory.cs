using Core.Common.ToolsHelper;
using log4net;
using System;
using System.IO;
using System.Web;
namespace Core.Log
{
    /// <summary>
    /// 日志初始化
    /// </summary>
    public class LogFactory
    {
        static LogFactory()
        {
            FileInfo configFile;
            if (HttpContext.Current != null)
            {
                configFile = new FileInfo(HttpContext.Current.Server.MapPath("/Configs/log4net.config"));
            }
            else
            {
                configFile = new FileInfo(Configs.GetValue("Log4netUrl"));
            }
            log4net.Config.XmlConfigurator.Configure(configFile);
        }
        public static Log GetLogger(Type type)
        {
            return new Log(LogManager.GetLogger(type));
        }
        public static Log GetLogger(string str)
        {
            return new Log(LogManager.GetLogger(str));
        }
    }
}
