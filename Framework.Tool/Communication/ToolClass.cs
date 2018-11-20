using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Management;
using System.Net;
using System.Xml;
using System.Diagnostics;
using System.Data;
using System.Collections;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using Framework.Common;

namespace Framework.Tool
{
    public class ToolClass
    {
        /// <summary>
        /// 将xml数据写入到xml文件中
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="strData"></param>
        /// <returns></returns>
        //public bool SaveXmlDataToFile(string strPath, string strData)
        //{
        //    bool flag = false;
        //    try
        //    {
        //        strPath = FileHelper.GetAppPath() + "\\" + strPath;
        //        int len = strPath.LastIndexOf('\\');
        //        string strDir = strPath.Substring(0, len);
        //        if (!Directory.Exists(strDir))
        //        {
        //            Directory.CreateDirectory(strDir);
        //        }
        //        if (!File.Exists(strPath))
        //        {
        //            FileStream stream = File.Create(strPath);
        //            stream.Close();
        //        }
        //        XmlDocument doc = new XmlDocument();
        //        doc.LoadXml(strData);
        //        doc.Save(strPath);
        //        flag = true;
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //    return flag;
        //}
        /// <summary>
        /// 将字符串转换16进制ASCII码字符串
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public string strToASCII16(string hexString)
        {
            byte[] ByteFoo = System.Text.Encoding.ASCII.GetBytes(hexString);

            string TempStr = String.Empty;
            foreach (byte b in ByteFoo)
            {
                TempStr += b.ToString("X"); //X表示十六进制显示
            }
            return TempStr;
        }
        /// <summary>
        /// 将16进制ASCII码字符串转换
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string ASCII16ToStr(string str)
        {
            char[] arr = str.ToCharArray();
            string restr = string.Empty;
            string a = string.Empty;
            for (int i = 0; i < arr.Length; i++)
            {
                a = arr[i].ToString() + arr[i + 1].ToString();
                if (a == "00")
                    break;
                restr += ((char)Convert.ToInt32(a, 16)).ToString();
                i++;
            }
            return restr;
        }
        /// <summary>
        /// 获取正在使用的方法名
        /// </summary>
        /// <returns></returns>
        public string GetFunName()
        {
            MethodBase mb = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            string name = mb.Name;
            string[] full = mb.DeclaringType.FullName.Split('.');
            return full[full.Length - 1] + "." + name;
        }
        /// <summary>
        ///异或计算
        /// </summary>
        /// <param name="part1">第一组数据</param>
        /// <param name="part2">第二组数据</param>
        /// <returns></returns>
        public string XOR(string part1, string part2)
        {
            byte[] byteP1 = TurnIntoByte(part1);
            byte[] byteP2 = TurnIntoByte(part2);
            var result = new byte[byteP1.Length];
            byteP1.CopyTo(result, 0);
            for (int i = 0; i < byteP1.Length; i++)
                result[i] ^= byteP2[i];
            string strResult = TurnIntoString(result);
            return strResult;
        }
        /// <summary>
        /// 将字符串按2个字符转换成16进制字节数组
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public byte[] TurnIntoByte(string key)
        {
            byte[] newByte = new byte[key.Length / 2];
            for (int i = 0; i < key.Length / 2; i++)
            {
                Int16 int16 = Convert.ToInt16(key.Substring(2 * i, 2), 16);
                byte b = Convert.ToByte(int16);
                newByte[i] = b;
            }
            return newByte;
        }
        /// <summary>
        /// 将银行卡号转换成pin计算的银行卡号
        /// 银行卡号从右边第二个去12长度,组成8字节长度 不足前面补零
        /// </summary>
        /// <param name="strBankNo">原银行卡号</param>
        /// <returns></returns>
        public string BankCardNoFormat(string strBankNo)
        {
            int len = strBankNo.Length;
            if (len <= 12)
            {
                strBankNo = strBankNo.PadLeft(12, '0');
            }
            else if (len == 16 && strBankNo.Substring(0, 4) == "0000")
            {
                strBankNo = strBankNo.Substring(4, 12);
            }
            else
            {
                strBankNo = strBankNo.Substring(len - 13, 12);
            }
            return strBankNo;
        }
        /// <summary>
        /// 将16进制字节数组转换成字符串
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public string TurnIntoString(byte[] Input)
        {
            StringBuilder sb_Output = new StringBuilder();
            for (int i = 0; i < Input.Length; i++)
            {
                sb_Output.Append(Input[i].ToString("X02"));
            }
            return sb_Output.ToString();
        }

        /// <summary>
        /// 将字符串不足16倍数以0填充
        /// </summary>
        /// <param name="Data">源字符串</param>
        /// <returns></returns>
        public string StringChange(string Data)
        {
            if (Data.Length % 16 != 0)
            {
                int length = Math.Abs(Data.Length % 16 - 16);
                string suffix = string.Empty;
                suffix = suffix.PadRight(length, '0');
                Data += suffix;
            }
            return Data;
        }
        /// <summary>
        /// 将字符串按2个字符转换成16进制字节数组
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public byte[] strToToHexByte(string key)
        {
            byte[] newByte = new byte[key.Length / 2];
            for (int i = 0; i < key.Length / 2; i++)
            {
                Int16 int16 = Convert.ToInt16(key.Substring(2 * i, 2), 16);
                byte b = Convert.ToByte(int16);
                newByte[i] = b;
            }
            return newByte;
        }
        /// <summary>
        /// 反射创建对象
        /// </summary>
        /// <param name="bllPath">当前目录下DLL名称</param>
        /// <param name="className">DLL命名空间下的类名</param>
        /// <returns></returns>
        public object CreateObject(string bllPath, string className)
        {
            object obj = null;
            try
            {
                obj = Assembly.LoadFile(bllPath).CreateInstance(className);
            }
            catch (Exception e) { }
            return obj;
        }

        /// <summary>
        /// 获取本机的mac地址
        /// </summary>
        /// <returns></returns>
        public static string GetMACAddress()
        {
            //获取网卡硬件地址 
            string mac = string.Empty;
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();//获取本机所有网卡对象
            foreach (NetworkInterface adapter in adapters)
            {
                if (adapter.Name.Equals("本地连接"))//枚举条件：描述中包含"Virtual"
                {
                    IPInterfaceProperties ipProperties = adapter.GetIPProperties();//获取IP配置
                    UnicastIPAddressInformationCollection ipCollection = ipProperties.UnicastAddresses;//获取单播地址集
                    foreach (UnicastIPAddressInformation ip in ipCollection)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)//只要ipv4的
                        {
                            mac = adapter.GetPhysicalAddress().ToString();
                        }
                    }
                }
            }
            string tempMac = "";
            if (mac.IndexOf(":") < 0)
            {
                for (int i = 0; i < mac.Length; i += 2)
                {
                    tempMac += mac.Substring(i, 2) + ":";
                }

            }
            tempMac = tempMac.Substring(0, tempMac.Length - 1);
            return tempMac;
        }

        /// <summary>
        /// 获取本机的MAC地址
        /// </summary>
        /// <returns></returns>
        public string GetDeviceMAC()
        {
            //获取网卡硬件地址 
            string mac = string.Empty;
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();//获取本机所有网卡对象
            foreach (NetworkInterface adapter in adapters)
            {
                if (adapter.Name.Equals("本地连接"))//枚举条件：描述中包含"Virtual"
                {
                    IPInterfaceProperties ipProperties = adapter.GetIPProperties();//获取IP配置
                    UnicastIPAddressInformationCollection ipCollection = ipProperties.UnicastAddresses;//获取单播地址集
                    foreach (UnicastIPAddressInformation ip in ipCollection)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)//只要ipv4的
                        {
                            mac = adapter.GetPhysicalAddress().ToString();
                        }
                    }
                }
            }
            string tempMac = "";
            if (mac.IndexOf(":") < 0)
            {
                for (int i = 0; i < mac.Length; i += 2)
                {
                    tempMac += mac.Substring(i, 2) + ":";
                }

            }
            tempMac = tempMac.Substring(0, tempMac.Length - 1);
            return tempMac;

        }

        /// <summary>
        /// 获取本机的IP地址
        /// </summary>
        /// <returns></returns>
        public string GetDeviceIP()
        {
            string strAddr = "";
            IPAddress ipAddress;
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();//获取本机所有网卡对象
            foreach (NetworkInterface adapter in adapters)
            {
                if (adapter.Name.Equals("本地连接"))//枚举条件：描述中包含"Virtual"
                {
                    IPInterfaceProperties ipProperties = adapter.GetIPProperties();//获取IP配置
                    UnicastIPAddressInformationCollection ipCollection = ipProperties.UnicastAddresses;//获取单播地址集
                    foreach (UnicastIPAddressInformation ip in ipCollection)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)//只要ipv4的
                            ipAddress = ip.Address;//获取ip
                        strAddr = ip.Address.ToString();
                    }
                }
            }
            return strAddr;
        }

        /// <summary>
        /// 根据身份证号码获取年龄
        /// </summary>
        /// <param name="IdCardNo"></param>
        /// <returns></returns>
        public string GetAgeByIDCardNo(string IdCardNo)
        {
            int birthday = 0; int nowtime = 0;
            if (IdCardNo.Length == 18)
            {
                birthday = Int32.Parse(IdCardNo.Substring(6, 8));
                nowtime = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));
            }
            if (IdCardNo.Length == 15)
            {

                birthday = Int32.Parse("19" + IdCardNo.Substring(6, 6));
                nowtime = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));
            }
            return ((nowtime - birthday) / 10000).ToString();

        }
        /// <summary>
        /// 根据身份证获取性别
        /// </summary>
        /// <param name="IdCardNo"></param>
        /// <returns></returns>
        public string GetSexByIDCardNo(string IdCardNo)
        {
            string strResult = string.Empty;
            string strSex = string.Empty;
            if (IdCardNo.Length == 18)
            {
                strSex = IdCardNo.Substring(14, 3);
            }
            if (IdCardNo.Length == 15)
            {
                strSex = IdCardNo.Substring(12, 3);
            }
            if (int.Parse(strSex) % 2 == 0)//性别代码为偶数是女性奇数为男性
            {
                strResult = "女";
            }
            else
            {
                strResult = "男";
            }
            return strResult;
        }

        /// <summary>
        /// 根据生日获取年龄
        /// </summary>
        /// <param name="Birthday"></param>
        /// <returns></returns>
        public string GetAgeByBirthday(string Birthday)
        {
            string Age = "";
            int birthday = 0; int nowtime = 0;
            if (!string.IsNullOrEmpty(Birthday))
            {
                birthday = Int32.Parse(Birthday.Replace("-", "").Replace("/", ""));
                nowtime = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));
                Age = ((nowtime - birthday) / 10000).ToString();
            }
            return Age;
        }

        /// <summary>
        /// 根据身份证号码获取生日
        /// </summary>
        /// <param name="IDCard"></param>
        public string GetBirthByIDCardNO(string IDCard)
        {
            string BirthDay = string.Empty;
            if (IDCard.Length == 15)
            {
                BirthDay = "19" + IDCard.Substring(6, 2) + "-" + IDCard.Substring(8, 2) + "-" + IDCard.Substring(10, 2);
            }

            if (IDCard.Length == 18)
            {
                BirthDay = IDCard.Substring(6, 4) + "-" + IDCard.Substring(10, 2) + "-" + IDCard.Substring(12, 2);
            }
            return BirthDay;
        }


        /// <summary>
        /// 执行关机命令
        /// </summary>
        /// <returns></returns>
        public bool ShutDown()
        {
            bool isok = true;
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.Start();
                p.StandardInput.WriteLine("shutdown -s -t 1");
                p.StandardInput.WriteLine("exit");
            }
            catch
            {
                isok = false;
            }
            return isok;
        }

        /// <summary>
        /// 执行重启命令
        /// </summary>
        /// <returns></returns>
        public bool ReStartUp()
        {
            bool isok = true;
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.Start();
                p.StandardInput.WriteLine("shutdown -r -t 1");
                p.StandardInput.WriteLine("exit");
            }
            catch
            {
                isok = false;
            }
            return isok;
        }

        /// <summary>
        /// 重启更新程序exe
        /// </summary>
        /// <param name="ExeName"></param>
        /// <returns></returns>
        public bool ReAutoUpdate(string ExeName)
        {
            bool isok = true;
            try
            {
                string AppName = Environment.CurrentDirectory + "\\" + ExeName + ".exe";
                Process.Start(AppName);

                //退出全部程序(强制退出,关闭所有后台线程);
                Environment.Exit(0);
            }
            catch
            {
                isok = false;
            }
            return isok;
        }

        /// <summary>
        /// 判断两个时间相差的天数
        /// </summary>
        /// <param name="dateStart">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <returns></returns>
        public int DateDiffToDay(DateTime dateStart, DateTime dateEnd)
        {
            DateTime start = Convert.ToDateTime(dateStart.ToShortDateString());
            DateTime end = Convert.ToDateTime(dateStart.ToShortDateString());
            TimeSpan sp = end.Subtract(start);
            return sp.Days;
        }

        /// <summary>
        /// 判断当前时间是否在两个指定的时间段内
        /// </summary>
        /// <param name="startTime">起始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public bool CheckRangeTime(string startTime, string endTime)
        {
            bool bFlag = false;
            DateTime dt_startTime = Convert.ToDateTime(DateTime.Parse(startTime).ToString("HH:mm:ss"));//开始时间
            DateTime dt_endTime = Convert.ToDateTime(DateTime.Parse(endTime).ToString("HH:mm:ss"));//结束时间
            DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));//当前时间
            TimeSpan t1 = (dtNow - dt_startTime);
            TimeSpan t2 = (dt_endTime - dtNow);
            if (t1.Seconds > 0 && t2.Seconds > 0)//如果在起始时间和结束时间段之内
            {
                bFlag = true;
            }
            return bFlag;
        }
        /// <summary>
        /// 根据属性名称获取对象属性值
        /// </summary>
        /// <param name="type">对象</param>
        /// <param name="AttributeName">属性名称</param>
        /// <returns></returns>
        public object GetAttributeValue(object obj, string AttributeName)
        {
            PropertyInfo pi = obj.GetType().GetProperties().FirstOrDefault(x => x.Name == AttributeName);
            return pi.GetValue(obj, null);
        }

        /// <summary>
        /// 根据表达式提取DataTable中符合条件的数据
        /// </summary>
        /// <param name="workflowXml">工作流xml</param>
        /// <param name="nodeName">结果节点名称</param>
        /// <param name="dtxml">接口返回的结果xml</param>
        /// <param name="dtNodeName">数据集table节点名称</param>
        /// <returns></returns>
        public string GetUrlByExpression(XmlDocument workflowXml, string nodeName, string dtxml, string dtNodeName, out string msg)
        {
            string url = string.Empty; msg = string.Empty;
            string Expression = XmlHelper.GetNodeAttribute(workflowXml, nodeName, "Expression").Trim();
            if (string.IsNullOrEmpty(Expression) || Convert.ToBoolean(Expression) == false)
            {
                url = XmlHelper.GetNodeAttribute(workflowXml, nodeName, "url");
            }
            else//有表达式
            {
                if (!string.IsNullOrEmpty(dtxml))
                {
                    DataSet ds = ConvertXMLToDataSet(dtxml);
                    XmlNodeList list = XmlHelper.GetNodeList(workflowXml, nodeName + "/Expression");
                    foreach (XmlNode node in list)
                    {
                        string value = (node.Attributes["value"] != null) ? node.Attributes["value"].InnerText : string.Empty;
                        if (!string.IsNullOrEmpty(value))//如果表达式不为空
                        {
                            DataRow[] rows = ds.Tables[dtNodeName].Select(value);
                            if (rows.Length > 0)
                            {
                                url = (node.Attributes["url"] != null) ? node.Attributes["url"].InnerText : string.Empty;
                                msg = (node.Attributes["msg"] != null) ? node.Attributes["msg"].InnerText : string.Empty;//获取提示信息
                                if (!WinContext.ArrList.Contains(value))
                                {
                                    WinContext.ArrList.Add(value);
                                }
                            }
                        }
                        //else
                        //{
                        //    url = XmlHelper.GetNodeAttribute(workflowXml, nodeName, "url");
                        //}
                    }
                }
            }
            return url;
        }

        /// <summary>
        /// 根据表达式提取DataTable中符合条件的数据
        /// </summary>
        /// <param name="workflowXml">工作流xml</param>
        /// <param name="nodeName">结果节点名称</param>
        /// <param name="dtxml">接口返回的结果xml</param>
        /// <param name="dtNodeName">数据集table节点名称</param>
        /// <param name="filter">过滤表达式</param>
        /// <returns></returns>
        public string GetUrlByExpression(XmlDocument workflowXml, string nodeName, string dtxml, string dtNodeName, string filter, out string msg)
        {
            string url = string.Empty; msg = string.Empty;
            string Expression = XmlHelper.GetNodeAttribute(workflowXml, nodeName, "Expression").Trim();
            if (string.IsNullOrEmpty(Expression) || Convert.ToBoolean(Expression) == false)
            {
                url = XmlHelper.GetNodeAttribute(workflowXml, nodeName, "url");
            }
            else//有表达式
            {
                if (!string.IsNullOrEmpty(dtxml))
                {
                    DataSet ds = ConvertXMLToDataSet(dtxml);
                    XmlNodeList list = XmlHelper.GetNodeList(workflowXml, nodeName + "/Expression");
                    foreach (XmlNode node in list)
                    {
                        string value = (node.Attributes["value"] != null) ? node.Attributes["value"].InnerText : string.Empty;
                        if (!string.IsNullOrEmpty(value))//如果表达式不为空
                        {
                            DataRow[] rows = ds.Tables[dtNodeName].Select(value + " and " + filter);
                            if (rows.Length > 0)
                            {
                                url = (node.Attributes["url"] != null) ? node.Attributes["url"].InnerText : string.Empty;
                                msg = (node.Attributes["msg"] != null) ? node.Attributes["msg"].InnerText : string.Empty;//获取提示信息
                                if (!WinContext.ArrList.Contains(value))
                                {
                                    WinContext.ArrList.Add(value);
                                }
                            }
                        }
                        //else
                        //{
                        //    url = XmlHelper.GetNodeAttribute(workflowXml, nodeName, "url");
                        //}
                    }
                }
            }
            return url;
        }



        /// <summary>
        /// 根据表达式提取DataTable中符合条件的数据
        /// </summary>
        /// <param name="workflowXml">工作流xml</param>
        /// <param name="nodeName">结果节点名称</param>
        /// <param name="dt">接口返回的结果xml</param>
        /// <param name="msg">输出信息</param>
        /// <returns></returns>
        public string GetUrlByExpression(XmlDocument workflowXml, string nodeName, DataTable dt, out string msg)
        {
            string url = string.Empty; msg = string.Empty;
            try
            {
                string Expression = XmlHelper.GetNodeAttribute(workflowXml, nodeName, "Expression").Trim();
                if (string.IsNullOrEmpty(Expression) || Convert.ToBoolean(Expression) == false)
                {
                    url = XmlHelper.GetNodeAttribute(workflowXml, nodeName, "url");
                }
                else//有表达式
                {
                    XmlNodeList list = XmlHelper.GetNodeList(workflowXml, nodeName + "/Expression");
                    foreach (XmlNode node in list)
                    {
                        string value = (node.Attributes["value"] != null) ? node.Attributes["value"].InnerText : string.Empty;
                        if (!string.IsNullOrEmpty(value))//如果表达式不为空
                        {
                            DataRow[] rows = dt.Select(value);
                            if (rows.Length > 0)
                            {
                                url = (node.Attributes["url"] != null) ? node.Attributes["url"].InnerText : string.Empty;
                                msg = (node.Attributes["msg"] != null) ? node.Attributes["msg"].InnerText : string.Empty;//获取提示信息
                                if (!WinContext.ArrList.Contains(value))
                                {
                                    WinContext.ArrList.Add(value);
                                }
                            }
                        }
                        else
                        {
                            url = XmlHelper.GetNodeAttribute(workflowXml, nodeName, "url");
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            return url;
        }

        /// <summary>
        /// 根据表达式提取DataTable中符合条件的数据
        /// </summary>
        /// <param name="workflowXml">工作流xml</param>
        /// <param name="nodeName">结果节点名称</param>
        /// <param name="dt">接口返回的结果xml</param>
        /// <param name="msg">输出信息</param>
        /// <returns></returns>
        public string GetUrlByExpression(XmlDocument workflowXml, string nodeName, ArrayList ArrList, out string msg)
        {
            string url = string.Empty; msg = string.Empty;
            try
            {
                string Expression = XmlHelper.GetNodeAttribute(workflowXml, nodeName, "Expression").Trim();
                if (string.IsNullOrEmpty(Expression) || Convert.ToBoolean(Expression) == false)
                {
                    url = XmlHelper.GetNodeAttribute(workflowXml, nodeName, "url");
                }
                else//有表达式
                {
                    XmlNodeList list = XmlHelper.GetNodeList(workflowXml, nodeName + "/Expression");
                    foreach (XmlNode node in list)
                    {
                        string value = (node.Attributes["value"] != null) ? node.Attributes["value"].InnerText : string.Empty;
                        if (!string.IsNullOrEmpty(value))//如果表达式不为空
                        {
                            if (ArrList.Contains(value))
                            {
                                url = (node.Attributes["url"] != null) ? node.Attributes["url"].InnerText : string.Empty;
                                msg = (node.Attributes["msg"] != null) ? node.Attributes["msg"].InnerText : string.Empty;//获取提示信息
                            }
                        }
                        else
                        {
                            url = XmlHelper.GetNodeAttribute(workflowXml, nodeName, "url");//表达式没有配置值
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            return url;
        }

        /// <summary>
        /// 将XML转换成DataTable
        /// </summary>
        /// <param name="xmlData">xml数据</param>
        /// <returns></returns>
        public DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch (Exception ex)
            {
                string strTest = ex.Message;
                return null;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        /// <summary>
        /// 根据实体类得到表结构
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        private DataTable ModelToDataTable<T>(T model)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                if (propertyInfo.Name != "CTimestamp")//些字段为oracle中的Timesstarmp类型
                {
                    dataTable.Columns.Add(new DataColumn(propertyInfo.Name, propertyInfo.PropertyType));
                }
                else
                {
                    dataTable.Columns.Add(new DataColumn(propertyInfo.Name, typeof(DateTime)));
                }
            }
            return dataTable;
        }

        /// <summary>
        /// 将实体类转换成DataTable
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public DataTable ConvertModelToDataTable(Type t)
        {
            DataTable dt = CreateData(t);
            DataRow dataRow = dt.NewRow();
            foreach (FieldInfo propertyInfo in t.GetFields())
            {
                dataRow[propertyInfo.Name] = propertyInfo.GetValue(t);
            }
            dt.Rows.Add(dataRow);
            return dt;
        }

        /// 将实体类转换成DataSet
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public DataSet ConvertModelToDataSet(Type t)
        {
            DataSet ds = new DataSet();
            foreach (Type type in t.GetNestedTypes())
            {
                DataTable dt = CreateData(type);
                DataRow dataRow = dt.NewRow();
                foreach (FieldInfo propertyInfo in type.GetFields())
                {
                    dataRow[propertyInfo.Name] = propertyInfo.GetValue(type);
                }
                dt.Rows.Add(dataRow);
                ds.Tables.Add(dt);
            }
            foreach (FieldInfo propertyInfo in t.GetFields())
            {
                if (propertyInfo.FieldType.FullName == "System.Data.DataSet")
                {
                    ds.Merge(propertyInfo.GetValue(t) as DataSet);
                }
                if (propertyInfo.FieldType.FullName == "System.Data.DataTable")
                {
                    ds.Tables.Add(propertyInfo.GetValue(t) as DataTable);
                }

            }
            return ds;
        }


        /// <summary>
        /// 根据实体类得到表结构
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        private DataTable CreateData(Type t)
        {
            DataTable dataTable = new DataTable(t.Name);
            foreach (FieldInfo propertyInfo in t.GetFields())
            {
                dataTable.Columns.Add(new DataColumn(propertyInfo.Name, propertyInfo.FieldType));
            }
            return dataTable;
        }


        /// <summary>
        /// 实体类给实体类赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pTargetObjSrc"></param>
        /// <param name="pTargetObjDest"></param>
        public void EntityToEntity<T>(T pTargetObjSrc, T pTargetObjDest)
        {
            try
            {
                foreach (var mItem in typeof(T).GetProperties())
                {
                    mItem.SetValue(pTargetObjDest, mItem.GetValue(pTargetObjSrc, new object[] { }), null);
                }
            }
            catch (NullReferenceException NullEx)
            {
                throw NullEx;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }



        /// <summary>
        /// 实体类复制
        /// </summary>
        /// <param name="objold">目标实体类</param>
        /// <param name="objnew">源实体类</param>
        public void EntityCopy(object objold, object objnew)
        {
            try
            {
                Type myType = objold.GetType(),
                        myType2 = objnew.GetType();
                PropertyInfo currobj = null;
                if (myType == myType2)
                {
                    PropertyInfo[] myProperties = myType.GetProperties();
                    for (int i = 0; i < myProperties.Length; i++)
                    {
                        currobj = objold.GetType().GetProperties()[i];
                        currobj.SetValue(objnew, currobj.GetValue(objold, null), null);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 实体类复制
        /// </summary>
        /// <param name="objold">目标实体类</param>
        /// <param name="objnew">源实体类</param>
        public void EntityValue(object objold, object objnew)
        {
            try
            {
                Type myType = objold.GetType(),
                     myType2 = objnew.GetType();
                PropertyInfo currobj = null;
                PropertyInfo[] myProperties = myType.GetProperties();
                for (int i = 0; i < myProperties.Length; i++)
                {
                    currobj = objold.GetType().GetProperties()[i];
                    currobj.SetValue(objnew, currobj.GetValue(objold, null), null);
                }
            }
            catch (Exception)
            {

            }
        }


        /// <summary>
        /// 截取xml中的一部分不包括节点
        /// </summary>
        /// <param name="xml">xml字符串</param>
        /// <param name="nodeName">xml节点名称</param>
        /// <returns>string</returns>
        public string SubStingToXML(string xml, string nodeName)
        {
            string strXml = string.Empty;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            strXml = doc.SelectSingleNode("Response/Body/" + nodeName).InnerXml;
            return strXml;
        }

        /// <summary>
        /// 读取记事本
        /// </summary>
        /// <param name="fileName">记事本名称</param>
        /// <returns></returns>
        public string ReadTxt(string fileName)
        {
            StreamReader sr = null;
            StringBuilder sb = new StringBuilder();
            try
            {
                using (sr = new StreamReader(fileName, System.Text.Encoding.Default))
                {
                    sb.Append(sr.ReadToEnd());
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                sr.Close();
            }

            return sb.ToString();
        }

        ///// <summary>
        ///// 读取打印目录的凭条记事本文件信息
        ///// </summary>
        ///// <param name="fileName">记事本名称</param>
        ///// <returns></returns>
        //public string ReadPrintTxt(string fileName)
        //{
        //    StreamReader sr = null;
        //    StringBuilder sb = new StringBuilder();
        //    try
        //    {
        //        string strpath = FileHelper.GetAppPath() + "\\PrintTemplate\\" + fileName + ".txt";
        //        using (sr = new StreamReader(strpath, System.Text.Encoding.Default))
        //        {
        //            sb.Append(sr.ReadToEnd());
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //    return sb.ToString();
        //}

        /// <summary>
        /// 读取打印目录的凭条记事本文件信息
        /// </summary>
        /// <param name="hspName">医院名称</param>
        /// <param name="fileName">记事本名称</param>
        /// <returns></returns>
        //public string ReadPrintTxt(string hspName, string fileName)
        //{
        //    StreamReader sr = null;
        //    StringBuilder sb = new StringBuilder();
        //    try
        //    {
        //        string strpath = FileHelper.GetAppPath() + "\\PrintTemplate\\" + hspName + "\\" + fileName + ".txt";
        //        using (sr = new StreamReader(strpath, System.Text.Encoding.Default))
        //        {
        //            sb.Append(sr.ReadToEnd());
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //    return sb.ToString();
        //}


        /// <summary> 
        /// 在指定的字符串列表CnStr中检索符合拼音索引字符串 
        /// </summary> 
        /// <param name="CnStr">汉字字符串</param> 
        /// <returns>相对应的汉语拼音首字母串</returns> 
        public string GetSpellCode(string CnStr)
        {
            string strTemp = "";
            int iLen = CnStr.Length;
            int i = 0;

            for (i = 0; i <= iLen - 1; i++)
            {
                strTemp += GetCharSpellCode(CnStr.Substring(i, 1));
            }

            return strTemp;
        }


        /// <summary> 
        /// 得到一个汉字的拼音第一个字母，如果是一个英文字母则直接返回大写字母 
        /// </summary> 
        /// <param name="CnChar">单个汉字</param> 
        /// <returns>单个大写字母</returns> 
        private string GetCharSpellCode(string CnChar)
        {
            long iCnChar;

            byte[] ZW = System.Text.Encoding.Default.GetBytes(CnChar);

            //如果是字母，则直接返回 
            if (ZW.Length == 1)
            {
                return CnChar.ToUpper();
            }
            else
            {
                // get the array of byte from the single char 
                int i1 = (short)(ZW[0]);
                int i2 = (short)(ZW[1]);
                iCnChar = i1 * 256 + i2;
            }

            //expresstion 
            //table of the constant list 
            // 'A'; //45217..45252 
            // 'B'; //45253..45760 
            // 'C'; //45761..46317 
            // 'D'; //46318..46825 
            // 'E'; //46826..47009 
            // 'F'; //47010..47296 
            // 'G'; //47297..47613 

            // 'H'; //47614..48118 
            // 'J'; //48119..49061 
            // 'K'; //49062..49323 
            // 'L'; //49324..49895 
            // 'M'; //49896..50370 
            // 'N'; //50371..50613 
            // 'O'; //50614..50621 
            // 'P'; //50622..50905 
            // 'Q'; //50906..51386 

            // 'R'; //51387..51445 
            // 'S'; //51446..52217 
            // 'T'; //52218..52697 
            //没有U,V 
            // 'W'; //52698..52979 
            // 'X'; //52980..53640 
            // 'Y'; //53689..54480 
            // 'Z'; //54481..55289 

            // iCnChar match the constant 
            if ((iCnChar >= 45217) && (iCnChar <= 45252))
            {
                return "A";
            }
            else if ((iCnChar >= 45253) && (iCnChar <= 45760))
            {
                return "B";
            }
            else if ((iCnChar >= 45761) && (iCnChar <= 46317))
            {
                return "C";
            }
            else if ((iCnChar >= 46318) && (iCnChar <= 46825))
            {
                return "D";
            }
            else if ((iCnChar >= 46826) && (iCnChar <= 47009))
            {
                return "E";
            }
            else if ((iCnChar >= 47010) && (iCnChar <= 47296))
            {
                return "F";
            }
            else if ((iCnChar >= 47297) && (iCnChar <= 47613))
            {
                return "G";
            }
            else if ((iCnChar >= 47614) && (iCnChar <= 48118))
            {
                return "H";
            }
            else if ((iCnChar >= 48119) && (iCnChar <= 49061))
            {
                return "J";
            }
            else if ((iCnChar >= 49062) && (iCnChar <= 49323))
            {
                return "K";
            }
            else if ((iCnChar >= 49324) && (iCnChar <= 49895))
            {
                return "L";
            }
            else if ((iCnChar >= 49896) && (iCnChar <= 50370))
            {
                return "M";
            }

            else if ((iCnChar >= 50371) && (iCnChar <= 50613))
            {
                return "N";
            }
            else if ((iCnChar >= 50614) && (iCnChar <= 50621))
            {
                return "O";
            }
            else if ((iCnChar >= 50622) && (iCnChar <= 50905))
            {
                return "P";
            }
            else if ((iCnChar >= 50906) && (iCnChar <= 51386))
            {
                return "Q";
            }
            else if ((iCnChar >= 51387) && (iCnChar <= 51445))
            {
                return "R";
            }
            else if ((iCnChar >= 51446) && (iCnChar <= 52217))
            {
                return "S";
            }
            else if ((iCnChar >= 52218) && (iCnChar <= 52697))
            {
                return "T";
            }
            else if ((iCnChar >= 52698) && (iCnChar <= 52979))
            {
                return "W";
            }
            else if ((iCnChar >= 52980) && (iCnChar <= 53640))
            {
                return "X";
            }
            else if ((iCnChar >= 53689) && (iCnChar <= 54480))
            {
                return "Y";
            }
            else if ((iCnChar >= 54481) && (iCnChar <= 55289))
            {
                return "Z";
            }
            else return ("?");
        }
        /// <summary>
        /// 身份证15位转换18位
        /// </summary>
        /// <param name="perIDSrc"></param>
        /// <returns></returns>
        public string IDNoConvert15To18(string perIDSrc)
        {

            int iS = 0;

            //加权因子常数 
            int[] iW = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            //校验码常数 
            string LastCode = "10X98765432";
            //新身份证号 
            string perIDNew;

            perIDNew = perIDSrc.Substring(0, 6);
            //填在第6位及第7位上填上‘1’，‘9’两个数字 
            perIDNew += "19";

            perIDNew += perIDSrc.Substring(6, 9);

            //进行加权求和 
            for (int i = 0; i < 17; i++)
            {
                iS += int.Parse(perIDNew.Substring(i, 1)) * iW[i];
            }

            //取模运算，得到模值 
            int iY = iS % 11;
            //从LastCode中取得以模为索引号的值，加到身份证的最后一位，即为新身份证号。 
            perIDNew += LastCode.Substring(iY, 1);

            return perIDNew;
        }

        ///   <summary>
        ///   给一个字符串进行MD5加密
        ///   </summary>
        ///   <param   name="strText">待加密字符串</param>
        ///   <returns>加密后的字符串</returns>
        //public string GetMD5strData(string ConvertString)
        //{
        //    return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(ConvertString, "MD5");
        //}
        /// <summary>
        /// 结束运行程序进程
        /// <param name="pName">进程名称</param>
        /// </summary>
        public void KillProcess(string pName)
        {
            foreach (Process p in Process.GetProcessesByName(pName))
            {
                if (!p.CloseMainWindow())
                {
                    p.Kill();
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

    }
}
