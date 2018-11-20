using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;

namespace Framework.Tool
{
    /// <summary>
    /// 服务代理返回实体类
    /// </summary>
    public class ServiceReturnValue
    {
        /// <summary>
        /// 返回值 0 表示成功 1 表示失败 2 表示异常 9 表示未执行
        /// </summary>
        public int IsOK { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 错误描述
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public string OutData { get; set; }
        /// <summary>
        /// 返回数据对象
        /// </summary>
        public object OutObject { get; set; }
    }
    public enum ProxyCode
    {
        WebService通讯 = 1,
        Socket通讯 = 2,
        返回请求数据 = 3
    }
    public class ServiceProxy
    {
        /// <summary>
        /// 获取服务返回数据
        /// </summary>
        /// <param name="funConfig">通讯接口通讯参数实例</param>
        /// <param name="strRequestXml">请求数据</param>
        /// <returns></returns>
        public ServiceReturnValue GetResponseData(FunConfigClass funConfig, string strRequestXml)
        {
            ServiceReturnValue frv = new ServiceReturnValue();
            try
            {
                if (funConfig.ProxyCode.PadLeft(2, '0') == ((int)ProxyCode.WebService通讯).ToString("00"))
                {
                    string hisResponse = MyServicesProxy.InvokeWebService(funConfig.AccessUrl, funConfig.FunName, new object[] { strRequestXml }).ToString();
                    //string hisResponse = WebServiceHelper.Invoke(funConfig.HisWebServiceUrl, funConfig.HisWebServiceFunName,
                    //    new object[] { strRequestXml });
                    if (string.IsNullOrEmpty(hisResponse))
                    {
                        frv.IsOK = 2;
                        frv.Code = "H_1002";
                        frv.Msg = "调用" + funConfig + "接口时,接收响应数据为空";
                    }
                    else
                    {
                        frv.IsOK = 0;
                        frv.Code = "H_0000";
                        frv.OutData = hisResponse;
                    }
                }
                else if (funConfig.ProxyCode.PadLeft(2, '0') == ((int)ProxyCode.Socket通讯).ToString("00"))
                {
                    string[] strArr = funConfig.AccessUrl.Split(':');
                    string strSocketIP = strArr[0];
                    int SocketPort = Convert.ToInt32(strArr[1]);
                    string hisResponse = SocketHelper.Call(strRequestXml, funConfig.PrefixLen, strSocketIP,
                        SocketPort, funConfig.Encode, funConfig.TimeOut, 1024 * 50);//funConfig.BufSize
                    if (string.IsNullOrEmpty(hisResponse))
                    {
                        frv.IsOK = 2;
                        frv.Code = "H_1003";
                        frv.Msg = "调用" + funConfig + "接口时,接收响应数据为空";
                    }
                    else
                    {
                        frv.IsOK = 0;
                        frv.Code = "H_0000";
                        frv.OutData = hisResponse;
                    }
                }
                else if (funConfig.ProxyCode.PadLeft(2, '0') == ((int)ProxyCode.返回请求数据).ToString("00"))
                {
                    frv.IsOK = 0;
                    frv.OutData = strRequestXml;
                }
                else
                {
                    frv.IsOK = 9;
                    frv.Code = "H_1004";
                    frv.Msg = funConfig.FunCode + "该业务方法配置的代理通讯方式暂不支持";
                }
            }
            catch (Exception ea)
            {
                frv.IsOK = 2;
                frv.Code = "H_1001";
                frv.Msg = ea.Message;
            }
            return frv;
        }

        /// <summary>
        /// 获取服务返回数据
        /// 支持WebService通讯方法中多个入参数
        /// Socket通讯默认为数组中第一个参数
        /// </summary>
        /// <param name="funConfig"></param>
        /// <param name="strRequestXml"></param>
        /// <returns></returns>
        public ServiceReturnValue GetResponseData(FunConfigClass funConfig, object[] strRequestXml)
        {
            ServiceReturnValue frv = new ServiceReturnValue();
            try
            {

                if (funConfig.ProxyCode.PadLeft(2, '0') == ((int)ProxyCode.WebService通讯).ToString("00"))
                {
                    object hisResponse = MyServicesProxy.InvokeWebService(funConfig.AccessUrl, funConfig.FunName, strRequestXml).ToString();
                    //string hisResponse = WebServiceHelper.Invoke(funConfig.HisWebServiceUrl, funConfig.HisWebServiceFunName,
                    //    strRequestXml);
                    if (hisResponse == null)
                    {
                        frv.IsOK = 2;
                        frv.Code = "H_1002";
                        frv.Msg = "调用" + funConfig + "接口时,接收响应数据为空";
                    }
                    else
                    {
                        frv.IsOK = 0;
                        frv.Code = "H_0000";
                        frv.OutData = hisResponse.ToString();
                        frv.OutObject = hisResponse;
                    }
                }
                else if (funConfig.ProxyCode.PadLeft(2, '0') == ((int)ProxyCode.Socket通讯).ToString("00"))
                {
                    string[] strArr = funConfig.AccessUrl.Split(':');
                    string strSocketIP = strArr[0];
                    int SocketPort = Convert.ToInt32(strArr[1]);
                    string hisResponse = SocketHelper.Call(strRequestXml[0].ToString(), funConfig.PrefixLen,
                        strSocketIP, SocketPort, funConfig.Encode, funConfig.TimeOut, funConfig.BufSize);
                    if (string.IsNullOrEmpty(hisResponse))
                    {

                        frv.IsOK = 2;
                        frv.Code = "H_1003";
                        frv.Msg = "调用" + funConfig + "接口时,接收响应数据为空";
                    }
                    else
                    {
                        frv.IsOK = 0;
                        frv.Code = "H_0000";
                        frv.OutData = hisResponse;
                    }
                }
                else if (funConfig.ProxyCode.PadLeft(2, '0') == ((int)ProxyCode.返回请求数据).ToString("00"))
                {
                    frv.IsOK = 0;
                    frv.OutData = strRequestXml[0].ToString();
                }
                else
                {
                    frv.IsOK = 9;
                    frv.Code = "H_1004";
                    frv.Msg = funConfig.FunCode + "该业务方法配置的代理通讯方式暂不支持";
                }
            }
            catch (Exception ea)
            {
                frv.IsOK = 2;
                frv.Code = "H_1001";
                frv.Msg = ea.Message;
            }
            return frv;
        }

        /// <summary>
        /// 获取服务返回数据
        /// 支持WebService通讯方法中多个入参数,入参带类型
        /// Socket通讯默认为数组中第一个参数
        /// </summary>
        /// <param name="funConfig"></param>
        /// <param name="strRequestXml">入参数据</param>
        /// <param name="types">入参类型</param>
        /// <returns></returns>
        public ServiceReturnValue GetResponseData(FunConfigClass funConfig, object[] strRequestXml, params Type[] types)
        {
            ServiceReturnValue frv = new ServiceReturnValue();
            try
            {

                if (funConfig.ProxyCode.PadLeft(2, '0') == ((int)ProxyCode.WebService通讯).ToString("00"))
                {
                    string hisResponse = MyServicesProxy.InvokeWebService(funConfig.AccessUrl, funConfig.FunName, strRequestXml, types).ToString();
                    //string hisResponse = WebServiceHelper.Invoke(funConfig.HisWebServiceUrl, funConfig.HisWebServiceFunName,
                    //    strRequestXml);
                    if (string.IsNullOrEmpty(hisResponse))
                    {
                        frv.IsOK = 2;
                        frv.Code = "H_1002";
                        frv.Msg = "调用" + funConfig + "接口时,接收响应数据为空";
                    }
                    else
                    {
                        frv.IsOK = 0;
                        frv.Code = "H_0000";
                        frv.OutData = hisResponse;
                    }
                }
                else if (funConfig.ProxyCode.PadLeft(2, '0') == ((int)ProxyCode.Socket通讯).ToString("00"))
                {
                    string[] strArr = funConfig.AccessUrl.Split(':');
                    string strSocketIP = strArr[0];
                    int SocketPort = Convert.ToInt32(strArr[1]);
                    string hisResponse = SocketHelper.Call(strRequestXml[0].ToString(), funConfig.PrefixLen,
                        strSocketIP, SocketPort, funConfig.Encode, funConfig.TimeOut, funConfig.BufSize);
                    if (string.IsNullOrEmpty(hisResponse))
                    {
                        frv.IsOK = 2;
                        frv.Code = "H_1003";
                        frv.Msg = "调用" + funConfig + "接口时,接收响应数据为空";
                    }
                    else
                    {
                        frv.IsOK = 0;
                        frv.Code = "H_0000";
                        frv.OutData = hisResponse;
                    }
                }
                else if (funConfig.ProxyCode.PadLeft(2, '0') == ((int)ProxyCode.返回请求数据).ToString("00"))
                {
                    frv.IsOK = 0;
                    frv.OutData = strRequestXml[0].ToString();
                }
                else
                {
                    frv.IsOK = 9;
                    frv.Code = "H_1004";
                    frv.Msg = funConfig.FunCode + "该业务方法配置的代理通讯方式暂不支持";
                }
            }
            catch (Exception ea)
            {
                frv.IsOK = 2;
                frv.Code = "H_1001";
                frv.Msg = ea.Message;
            }
            return frv;
        }
    }
}
