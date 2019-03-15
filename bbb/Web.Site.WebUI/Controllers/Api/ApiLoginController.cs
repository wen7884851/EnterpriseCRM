using PubSerRMS.Core.Service.Authen;
using PubSerRMS.Core.Service.SysConfig;
using PubSerRMS.Site.ApiModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData.Query;
using PubSer.Framework.Tool;
using PubSerRMS.Domain.Models;
using Newtonsoft.Json;
using PubSer.Framework.Common.Operator;
using PubSer.Framework.Common.ToolsHelper.Net;
using System.Text;
using PubSerRMS.Domain.Models.Authen;
using PubSer.Framework.Common.ToolsHelper;
using PubSerRMS.Domain.Models.SysConfig;
using PubSer.Framework.Common.Cache.Factory;

namespace PubSerRMS.Site.WebUI.Controllers.Api
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ApiLoginController : ApiController
    {
        #region 属性
        [Import]
        IUserService UserService { get; set; }
        [Import]
        ISystemSiteService SystemSiteService { get; set; }
        [Import]



















        IResourcesService ResourcesService { get; set; }
        [Import]
        ISessionLogService SessionLogService { get; set; }
        #endregion
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage { Content = new StringContent("接口调用成功", Encoding.GetEncoding("UTF-8"), "text/plain") };
        }

        public HttpResponseMessage Post(string Action,string Content)
        {
            OperationResult Msg = new OperationResult();
            string[] name = Content.Split(',');
            try
            {
                switch (Convert.ToInt32(Action))
                {
                    case (int)ApiActionType.InLogin:
                        Msg = LoginCheck(name[0], name[1]);
                        break;
                    case (int)ApiActionType.OutLogin:
                        Msg = OutLogin(Content);
                        break;
                    case (int)ApiActionType.CheckSession:
                        Msg = CheckSession(Content);
                        break;
                    default:
                        Msg.ResultType = OperationResultType.QueryNull;
                        Msg.Message = "Action参数错误，没有找到相应方法";
                        break;
                }
            }
            catch(Exception e)
            {
                Msg.ResultType = OperationResultType.ParamError;
                Msg.Message = e.Message;
            }
         return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(Msg), Encoding.GetEncoding("UTF-8"), "text/plain") };
        }



        OperationResult LoginCheck(string UserName, string SystemGid)
        {
            var Usermodel = UserService.Users.Where(t => t.LoginName == UserName && t.IsDeleted == false && t.IsLoginName == true).ToList();
            if (Usermodel.Count != 1)
            {
                return new OperationResult(OperationResultType.Error, "登录名重复");
            }
           var SystemSitemodel= SystemSiteService.SystemSites.Where(t => t.Gid == SystemGid && t.IsDeleted == false).FirstOrDefault();
           var Resourcesmodel = ResourcesService.Resourcess.Where(t => t.ResourcesType == (int)ResourcesType.SystemForUser &&
             t.SystemId == SystemSitemodel.Id && t.ResourcesId == Usermodel[0].Id && t.IsDeleted == false).FirstOrDefault();
            if (Resourcesmodel == null)
            {
                return new OperationResult(OperationResultType.Error, "用户无登录系统权限");
            }
            return new OperationResult(OperationResultType.Success, InLoginSession(UserName, SystemGid).Message);
        }

        OperationResult InLoginSession(string UserName, string SystemGid)
        {
                var SessionLogmodel = SessionLogService.SessionLogs.Where(t => t.UserName == UserName
           && t.SystemGid == SystemGid && t.IsDeleted == false).FirstOrDefault();
                SessionLog NewSession = BuildSession(UserName, SystemGid);
                if (SessionLogmodel != null)    //查询SessionLog是否存在，存在则移除之前缓存，建立新缓存
                {
                    CacheFactory.Cache().RemoveCache(SessionLogmodel.SessionID);
                    SessionLogService.Delete(SessionLogmodel);
                }
                CacheFactory.Cache().WriteCache(NewSession, NewSession.SessionID, DateTime.Now.AddMinutes(60));
                SessionLogService.Insert(NewSession);
            return new OperationResult(OperationResultType.Success, NewSession.SessionID);

        }

        SessionLog BuildSession(string UserName, string SystemGid)
        {
            User SubUser = ResourcesService.GetSubUser(UserName, SystemGid,(int)ResourcesType.SystemForUser);
            return new SessionLog
            {
                SessionID = Guid.NewGuid().ToString(),
                UserName = UserName,
                SystemGid = SystemGid,
                SubUserName = SubUser.LoginName,
                Ip = Net.Ip,
                CreateTime=DateTime.Now,
                ActiveTime= DateTime.Now.AddHours(1),
                IsDeleted=false
            };
        }

        OperationResult OutLogin(string SessionId)
        {
            var SessionLogmodel = SessionLogService.SessionLogs.Where(t =>t.SessionID== SessionId
             && t.IsDeleted == false).FirstOrDefault();
            if(SessionLogmodel!=null)
            {
                CacheFactory.Cache().RemoveCache(SessionLogmodel.SessionID);
                SessionLogService.Delete(SessionLogmodel);
            }
            return new OperationResult(OperationResultType.Success, "操作成功");
        }

        OperationResult CheckSession(string SessionId)
        {
            try
            {
                SessionLog SessionLogmodel = CacheFactory.Cache().GetCache<SessionLog>(SessionId);

              
                if (SessionLogmodel == null)
                {
                    #region 缓存失效时
                    SessionLogmodel = SessionLogService.SessionLogs.Where(t => t.SessionID == SessionId
              && t.IsDeleted == false).FirstOrDefault();
                    if(SessionLogmodel==null)
                    {
                        return new OperationResult(OperationResultType.QueryNull, "登录失效，请重新登录");
                    }
                    if(SessionLogmodel.ActiveTime>DateTime.Now)
                    {
                        return new OperationResult(OperationResultType.QueryNull, "登录信息超时，请重新登录");
                    }
                    CacheFactory.Cache().WriteCache(SessionLogmodel, SessionLogmodel.SessionID, DateTime.Now.AddMinutes(60));
                    #endregion
                }

                return new OperationResult(OperationResultType.Success, "登录有效，请继续操作");
            }
           catch(Exception e)
            {
                return new OperationResult(OperationResultType.Error, "缓存读取失败："+e.Message);
            }
        }
    }
}
