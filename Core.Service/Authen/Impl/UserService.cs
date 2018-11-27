using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Repository.Authen;
using Domain.DB.Models;
using Domain.Site.Models;
using Domain.Site.Models.Authen.User;
using Framework.Common.SecurityHelper;
using Framework.Common.ToolsHelper;
using Framework.Common.ToolsHelper.Net;
using Framework.Tool.Operator;

namespace Core.Service.Authen.Impl
{
    /// <summary>
    /// 服务层实现 —— UserService
    /// </summary>
    [Export(typeof(IUserService))]
    public class UserService : CoreServiceBase, IUserService
    {
        #region 属性

        [Import]
        public IUserRepository UserRepository { get; set; }
      
        public IQueryable<User> Users
        {
            get { return UserRepository.NoCahecEntities; }
        }
        #endregion

        #region 公共方法
        public UserViewModel GetUserModelByToken(string token)
        {
            var user = Users.FirstOrDefault(t => t.Token == token);
            if (user != null)
            {
                return Mapper.Map<UserViewModel>(user);
            }
            return null;
        }
        public UserAccountViewModel GetAccountByLoginName(string LoginName)
        {
            var user =Users.FirstOrDefault(t => t.LoginName == LoginName || t.Email == LoginName || t.Phone == LoginName);
            if(user!=null)
            {
                return new UserAccountViewModel()
                {
                    userId=user.Id,
                    LoginName=user.LoginName,
                    LoginPwd=user.LoginPwd,
                    PwdErrorCount=user.PwdErrorCount,
                    Enabled=user.Enabled,
                    LastLoginTime=user.LastLoginTime
                };
            }
            return null;
        }
        public UserViewModel GetUserModelById(int userId)
        {
            var user = Users.FirstOrDefault(t => t.Id == userId);
            if (user != null)
            {
               return  Mapper.Map<UserViewModel>(user);
            }
            return null;
        }


        public ActionResultViewModel CheckLogin(UserAccountViewModel user)
        {
            var result = new ActionResultViewModel()
            {
                IsSuccess = false,
            };

            if (string.IsNullOrEmpty(user.LoginName) && string.IsNullOrEmpty(user.LoginPwd))
            {
                result.Result = "用户名密码为空，请重新输入！";
                return result;
            }
            var userAccount = GetAccountByLoginName(user.LoginName);
            if (userAccount == null)
            {
                result.Result = "用户名不存在，请确定后重新输入！";
                return result;
            }
            if (userAccount.PwdErrorCount > 5 && userAccount.LastLoginTime < System.DateTime.Now.AddMinutes(-5))
            {
                result.Result = "用户名输入密码错误次数超过5次，请5分钟后再登陆！";
                return result;
            }
            if (userAccount.LoginPwd != MD5Provider.GetMD5String(user.LoginPwd))
            {
                result.Result = "密码错误，请确定后重新输入！";
                UpdateUserLoginError(userAccount.userId);
                return result;
            }
            if(!userAccount.Enabled)
            {
                result.Result = "该用户已禁用！";
                return result;
            }
            Login(userAccount.userId);
            return result;
        }
        #endregion

        #region 私有方法
        private void UpdateUserLoginError(int userId)
        {
            var user = Users.FirstOrDefault(t => t.Id == userId);
            if (user.PwdErrorCount<=5)
            {
                user.PwdErrorCount++;
            }
            else
            {
                if(user.LastLoginTime < System.DateTime.Now.AddMinutes(-5))
                {
                    user.LastLoginTime = System.DateTime.Now;
                    user.PwdErrorCount = 1;
                }
            }
            UserRepository.Update(user);
        }

        private void Login(int userId,bool isRemeberUserName=false)
        {
            var user = Users.FirstOrDefault(t => t.Id == userId);
            user.IsRemeberUserName = isRemeberUserName;
            user.Token = Guid.NewGuid().ToString();
            var result = Mapper.Map<OperatorModel>(user);
            result.LoginIPAddress = Net.Ip;
            result.LoginIPAddressName = Net.Host;
            OperatorProvider.Provider.AddCurrent(result);
            UserRepository.Update(user);
        }
        #endregion
    }
}
