
using Framework.Tool;
using System;
using System.Configuration;
using Framework.Common.ToolsHelper;
using Framework.Common.SecurityHelper;
using Framework.Common;
using Framework.Common.JsonHelper;
using Framework.Common.ToolsHelper.Net;
using System.Threading.Tasks;

namespace Framework.Tool.Operator
{
    public class OperatorProvider
    {
        public static OperatorProvider Provider
        {
            get { return new OperatorProvider(); }
        }

        private string LoginUserKey = "pubser_loginuserkey";
        private string LoginProvider = Configs.GetValue("LoginProvider");

        /// <summary>
        /// 当前用户
        /// </summary>
        /// <returns></returns>
        public OperatorModel GetCurrent()
        {
            var operatorModel = new OperatorModel();
            if (LoginProvider == "Cookie")
            {
                operatorModel = (CookieHelper.GetCookieValue(LoginUserKey).ToString()).ToObject<OperatorModel>();
                if(operatorModel==null)
                {
                    throw new AggregateException("用户过期需要重新登录");
                }
            }
            else
            {
                operatorModel = DESProvider.DecryptString(SessionHelper.GetSession(LoginUserKey).ToString()).ToObject<OperatorModel>();
            }
            return operatorModel;
        }

        /// <summary>
        /// 写入登录信息
        /// </summary>
        /// <param name="user">成员信息</param>
        public void AddCurrent(OperatorModel operatorModel)
        {
            if (LoginProvider == "Cookie")
            {
                CookieHelper.SetCookie(LoginUserKey, operatorModel.ToJson(), DateTime.Now.AddMinutes(60));
            }
            else
            {
                SessionHelper.Add(LoginUserKey, DESProvider.EncryptString(operatorModel.ToJson()),60);
            }
        }


        /// <summary>
        /// 删除登录信息
        /// </summary>
        public void RemoveCurrent()
        {
            if (LoginProvider == "Cookie")
            {
                CookieHelper.ClearCookie(LoginUserKey.Trim());
            }
            else
            {
                SessionHelper.Del(LoginUserKey.Trim());
            }
        }


        /// <summary>
        /// 是否过期
        /// </summary>
        /// <returns></returns>
        public virtual bool IsOverdue()
        {
            try
            {
                object str = "";
                if (LoginProvider == "Cookie")
                {
                    str = CookieHelper.GetCookieValue(LoginUserKey);
                }
                else
                {
                    str = SessionHelper.GetSession(LoginUserKey).ToString();
                }

                if (str != null && str.ToString() != "")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch 
            {
                return true;
            }
           


        }
        /// <summary>
        /// 是否已登录
        /// </summary>
        /// <returns></returns>
        public virtual int IsOnLine()
        {
            OperatorModel operatorModel = new OperatorModel();
            if (LoginProvider == "Cookie")
            {
                operatorModel = DESProvider.DecryptString(CookieHelper.GetCookieValue(LoginUserKey).ToString()).ToObject<OperatorModel>();
            }
            else
            {
                operatorModel = DESProvider.DecryptString(SessionHelper.GetSession(LoginUserKey).ToString()).ToObject<OperatorModel>();
            }

            object token = CacheFactory.Cache().GetCache<string>(operatorModel.UserId.ToString());
            if (token == null)
            {
                return -1;//过期
            }
            if (operatorModel.Token == token.ToString())
            {
                return 1;//正常
            }
            else
            {
                return 0;//已登录
            } 
        }


        /// <summary>
        /// 是否已登录子系统
        /// </summary>
        /// <returns></returns>
        public virtual int IsOnLineSS()
        {
            OperatorModel operatorModel = new OperatorModel();
            if (LoginProvider == "Cookie")
            {
                operatorModel = DESProvider.DecryptString(CookieHelper.GetCookieValue(LoginUserKey).ToString()).ToObject<OperatorModel>();
            }
            else
            {
                operatorModel = DESProvider.DecryptString(SessionHelper.GetSession(LoginUserKey).ToString()).ToObject<OperatorModel>();
            }
            string ApiUrl = ConfigurationManager.AppSettings["ApiLonginUrl"].ToString();
            //当点击登陆子系统才验证
            if (operatorModel.SessionID != null && operatorModel.SystemGid != null)
            {
                var strmsg = "";

                var Msg = strmsg.ToObject<OperationResult>();
                if (Msg.ResultType == OperationResultType.Error)
                {
                    return 0;//已登录
                }
                else if (Msg.ResultType == OperationResultType.QueryNull)
                {
                    return -1;//过期
                }
            }
            
            return 1;//正常
          


        }
    }
}
