using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Xml;
using Framework.Common.FileHelper;

namespace Framework.Log
{
    /// <summary>
    /// 日志
    /// </summary>
    public class LogUtil
    {
        private static Queue QUE_LOGS = Queue.Synchronized(new Queue());
        private static DateTime OLD_FILE_LOG_TIME = DateTime.Now;
        private static string _appPath = string.Empty;
        private static string _logPathPrefix = string.Empty;
        static Thread _T_LOGS = null;

        static LogUtil()
        {
            //应用程序路径

            _appPath = Assembly.GetExecutingAssembly().Location;

            _T_LOGS = new Thread(SaveLogs);
            _T_LOGS.IsBackground = true;
            _T_LOGS.Start();



            DeleteOldLog();
        }

        /// <summary>
        /// 设置日志路径前缀（""）
        /// </summary>
        /// <param name="logPathPrefix"></param>
        public static void SetLogPathPrefix(string logPathPrefix)
        {
            _logPathPrefix = logPathPrefix;
        }

        public static bool SaveXmlDataToFile(string fileName, string strData)
        {
            bool flag = false;
            try
            {
                string strPath = "D:\\FileLog\\" + fileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss.fff") + ".xml";
                int len = strPath.LastIndexOf('\\');
                string strDir = strPath.Substring(0, len);
                if (!Directory.Exists(strDir))
                {
                    Directory.CreateDirectory(strDir);
                }
                if (!File.Exists(strPath))
                {
                    FileStream stream = File.Create(strPath);
                    stream.Close();
                }
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(strData);
                doc.Save(strPath);
                flag = true;
            }
            catch (Exception e)
            {

            }
            return flag;
        }


        /// <summary>
        /// 写日志 【接口说明】【接口名称】【开始时间】【结束时间】【结果】【入参】【出参】【错误信息】
        /// </summary>
        /// <param name="logs">日志字符串数组</param>
        public static bool Write(string[] logs)
        {
            if (logs == null) return false;
            string funName = GetFullFunName();
            bool isok = true;
            StringBuilder strLog = new StringBuilder();
            strLog.Append("所属业务: 【 {9} 】 \r\n");
            strLog.Append("接口说明: 【 {0} 】 \r\n");
            strLog.Append("接口名称: 【 {1} 】 \r\n");
            strLog.Append("开始时间: 【 {2} 】 \r\n");
            strLog.Append("结束时间: 【 {3} 】 \r\n");
            strLog.Append("执行结果: 【 {4} 】 \r\n");
            strLog.Append("请求入参: 【 {5} 】 \r\n");
            strLog.Append("返回参数: 【 {6} 】 \r\n");
            strLog.Append("错误信息: 【 {7} 】 \r\n");
            strLog.Append("异常信息: 【 {8} 】 \r\n");
            strLog.Append("----------------------------------------------------------------------------------\r\n");
            string log = string.Format(strLog.ToString(), logs[0], funName, logs[1], logs[2], logs[3], logs[4], logs[5], logs[6], logs[7], logs[8]);

            //写日志到输出窗口，供调试使用
            //Console.WriteLine(log);            
            try
            {
                QUE_LOGS.Enqueue(log);
            }
            catch (Exception ex)
            {
                isok = false;
                //日志无法成功时显示在输出窗口，供调试使用
                //Console.WriteLine(ex.Message);
            }
            return isok;
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logs">【开始时间】【结束时间】【结果】【函数名称】【入参】【出参】【错误信息】</param>
        /// <param name="funName">方法名</param>
        /// <returns></returns>
        public static bool Write(string[] logs, string funName)
        {
            if (logs == null) return false;
            bool isok = true;
            StringBuilder strLog = new StringBuilder();
            strLog.Append("所属业务: 【 {9} 】 \r\n");
            strLog.Append("接口说明: 【 {0} 】 \r\n");
            strLog.Append("接口名称: 【 {1} 】 \r\n");
            strLog.Append("开始时间: 【 {2} 】 \r\n");
            strLog.Append("结束时间: 【 {3} 】 \r\n");
            strLog.Append("执行结果: 【 {4} 】 \r\n");
            strLog.Append("请求入参: 【 {5} 】 \r\n");
            strLog.Append("返回参数: 【 {6} 】 \r\n");
            strLog.Append("错误信息: 【 {7} 】 \r\n");
            strLog.Append("异常信息: 【 {8} 】 \r\n");
            strLog.Append("----------------------------------------------------------------------------------\r\n");
            string log = string.Format(strLog.ToString(), logs[0], funName, logs[1], logs[2], logs[3], logs[4], logs[5], logs[6], logs[7], logs[8]);
            try
            {
                QUE_LOGS.Enqueue(log);
            }
            catch (Exception ex)
            {
                isok = false;
            }
            return isok;
        }

        /// <summary>
        /// 记录异常日志
        /// </summary>
        /// <param name="errorMessage">异常信息</param>
        /// <returns></returns>
        public static bool Write(string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage)) return false;
            string funName = GetFullFunName();
            bool isok = true;
            StringBuilder strLog = new StringBuilder();
            strLog.Append("接口名称: 【 {0} 】 \r\n");
            strLog.Append("开始时间: 【 {1} 】 \r\n");
            strLog.Append("结束时间: 【 {2} 】 \r\n");
            strLog.Append("执行结果: 【 {3} 】 \r\n");
            strLog.Append("请求入参: 【 {4} 】 \r\n");
            strLog.Append("返回参数: 【 {5} 】 \r\n");
            strLog.Append("错误信息: 【 {6} 】 \r\n");
            strLog.Append("----------------------------------------------------------------------------------\r\n");
            string log = string.Format(strLog.ToString(), funName, DateTime.Now.ToString("HH:mm:ss.fff"), DateTime.Now.ToString("HH:mm:ss.fff"), "异常", "无", "无", errorMessage);

            //写日志到输出窗口，供调试使用
            //Console.WriteLine(log);
            try
            {
                QUE_LOGS.Enqueue(log);
            }
            catch (Exception ex)
            {
                isok = false;

                //日志无法成功时显示在输出窗口，供调试使用
                //Console.WriteLine(ex.Message);
            }
            return isok;
        }

        /// <summary>
        /// 获取方法名称
        /// </summary>
        /// <returns></returns>
        private static string GetFullFunName()
        {
            MethodBase mb = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod();
            string name = mb.Name;
            string[] full = mb.DeclaringType.FullName.Split('.');
            return full[full.Length - 1] + "." + name;
        }


        /// <summary>
        /// 删除一年前的旧日志
        /// </summary>
        private static void DeleteOldLog()
        {
            DateTime dt = DateTime.Now;
            //   string logPath = _appPath + "\\SelfLogs\\";
            string logPath = "D:\\SelfLogs\\";
            for (int i = 10; i > 1; i--)
            {
                string path = logPath + (dt.Year - i);
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
        }

        /// <summary>
        /// 保存日志文件
        /// </summary>
        private static void SaveLogs()
        {
            string logPath = "D:\\SelfLogs";

            StringBuilder sbstr = new StringBuilder();
            while (true)
            {
                try
                {
                    int count = 0;
                    while (QUE_LOGS.Count > 0)
                    {
                        string log = QUE_LOGS.Dequeue() as string;
                        sbstr.Append(log);
                        count++;
                        if (count == 5)
                        {
                            count = 0;
                            break;
                        }
                    }
                    if (sbstr.Length > 0)
                    {
                        DateTime dt = DateTime.Now;
                        if ((dt - OLD_FILE_LOG_TIME).TotalSeconds >= 60 * 60)//每60分钟新建一个日志文件
                        {
                            OLD_FILE_LOG_TIME = dt;
                        }
                        string datePath = dt.Year.ToString();
                        datePath += "\\" + dt.Month + "\\" + dt.Day + "\\";
                        //路径前缀
                        if (!string.IsNullOrEmpty(_logPathPrefix))
                        {
                            datePath += _logPathPrefix;
                        }
                        //DirFile.CreateDir(logPath + "\\" + datePath);
                        if (!Directory.Exists(logPath + "\\" + datePath))
                            Directory.CreateDirectory(logPath + "\\" + datePath);
                        string savePath = logPath + "\\" + datePath + OLD_FILE_LOG_TIME.ToString("yyyMMdd_HHmmss") + ".log";
                        if (DirFile.IsExistFile(savePath))
                        {
                            int Save = (DirFile.GetFileSize(savePath) / 1024);
                            if (Save > 1024)
                            {
                                OLD_FILE_LOG_TIME= DateTime.Now;
                            }
                        }
                        //   DirFile.WriteText(savePath, sbstr.ToString(), System.Text.Encoding.UTF8);
                        DirFile.AppendText(savePath, sbstr.ToString());
                        sbstr.Clear();
                    }
                }
                catch (Exception ex)
                {
                    //日志无法成功时显示在输出窗口，供调试使用
                    Console.WriteLine("保存日志失败：" + ex.Message);
                }

                //   Thread.Sleep(10);
            }
        }


    }
}
