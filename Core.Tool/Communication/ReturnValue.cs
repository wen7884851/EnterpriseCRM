//**********************************
//*ClassName:
//*Version:
//*Date:
//*Author:
//*Effect:
//**********************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Tool
{
    /// <summary>
    /// 执行结果枚举
    /// </summary>
    public enum ResultCode
    {
        成功 = 0,
        失败 = 1,
        异常 = 2,
        未执行 = 3,
        超时 = 9
    }
    public class ReturnValue
    {
        private ResultCode _IsOK = ResultCode.失败;

        /// <summary>
        /// 执行结果码
        /// </summary>
        public ResultCode IsOK
        {
            get { return _IsOK; }
            set { _IsOK = value; }
        }
        private string _Code = string.Empty;
        /// <summary>
        /// 状态码
        /// </summary>
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        private string _ErrorCode = string.Empty;
        /// <summary>
        /// 错误码，通过改码定位出现问题的位置
        /// </summary>
        public string ErrorCode
        {
            get { return _ErrorCode; }
            set { _ErrorCode = value; }
        }
        private string _Msg;

        /// <summary>
        /// 响应信息
        /// </summary>
        public string Msg
        {
            get { return _Msg; }
            set { _Msg = value; }
        }
        private string _EMsg = string.Empty;
        /// <summary>
        /// 异常信息
        /// </summary>
        public string EMsg
        {
            get { return _EMsg; }
            set { _EMsg = value; }
        }
        private string _Value = string.Empty;
        /// <summary>
        /// 返回值
        /// </summary>
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        private object _OutData;
        /// <summary>
        /// 返回数据对象
        /// </summary>
        public object OutData
        {
            get { return _OutData; }
            set { _OutData = value; }
        }

        private string _InParams = string.Empty;
        /// <summary>
        /// 请求数据
        /// </summary>
        public string InParams
        {
            get { return _InParams; }
            set { _InParams = value; }
        }

        private string _FunDescription = string.Empty;
        /// <summary>
        /// 方法名称描述
        /// </summary>
        public string FunDescription
        {
            get { return _FunDescription; }
            set { _FunDescription = value; }
        }

        private string _BeginTime = DateTime.Now.ToString("HH:mm:ss.fff");
        /// <summary>
        /// 开始时间
        /// </summary>
        public string BeginTime
        {
            get { return _BeginTime; }
            set { _BeginTime = value; }
        }

        private string _EndTime = DateTime.Now.ToString("HH:mm:ss.fff");
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }

        private string _BusTypeName = string.Empty;
        /// <summary>
        /// 所属业务类型
        /// </summary>
        public string BusTypeName
        {
            get { return _BusTypeName; }
            set { _BusTypeName = value; }
        }

        public override string ToString()
        {
            return base.ToString();
        }
        private object[] _ValueList;

        public object[] ValueList
        {
            get
            {
                if (_ValueList != null) { return _ValueList; }
                else {
                    if (string.IsNullOrEmpty(this.Value)) { return null; }
                    else return _ValueList = new object[] { this.Value };
                }
            }
            set { _ValueList = value; }
        }

        /// <summary>
        /// 将ReturnValue实体转换为数组日志格式
        /// </summary>
        /// <returns></returns>
        public string[] ToStrArry()
        {
            string strResult = this._IsOK.ToString();
            string beginTime = string.IsNullOrEmpty(this.BeginTime) ? DateTime.Now.ToString("HH:mm:ss.fff") : this.BeginTime;
            string endTime = string.IsNullOrEmpty(this.EndTime) ? DateTime.Now.ToString("HH:mm:ss.fff") : this.EndTime;
            string strInparam = string.IsNullOrEmpty(this.InParams) ? "无" : this.InParams;
            string strOut = string.IsNullOrEmpty(Convert.ToString(this.Value)) ? "无" : this.Value.ToString();
            string strMsg = string.IsNullOrEmpty(this.Msg) ? "" : this.Msg;
            string strEmsg = string.IsNullOrEmpty(this.EMsg) ? "" : this.EMsg;
            string strFunMemo = string.IsNullOrEmpty(this.FunDescription) ? "" : this.FunDescription;
            string strErrorCode = string.IsNullOrEmpty(this.ErrorCode) ? "" : this.ErrorCode;
            string strBusTypeName = string.IsNullOrEmpty(this.BusTypeName) ? "系统业务流程" : this.BusTypeName;
            string[] strArry = new string[] { strFunMemo, beginTime, endTime, strResult, strInparam, strOut, strMsg, strEmsg, strBusTypeName };
            return strArry;
        }

    }

    public class ServiceStaticClass
    {
        /// <summary>
        /// 存放接口代理列表
        /// </summary>
    
        public static Dictionary<string, FunConfigClass> FunConfigList = new Dictionary<string, FunConfigClass>();
        /// <summary>
        /// 接口通讯代理对象
        /// </summary>
 
        public static ServiceProxy ServiceProxy = new ServiceProxy();
    }
}
