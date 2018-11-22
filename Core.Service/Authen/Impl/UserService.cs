using System.ComponentModel.Composition;
using System.Linq;
using AutoMapper;
using Core.Repository.Authen;
using Domain.DB.Models;
using Domain.Site.Models;
using Domain.Site.Models.Authen.User;

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
            get { return UserRepository.Entities; }
        }
        #endregion

        #region 公共方法

        public UserAccountModel GetAccountByLoginName(string LoginName)
        {
            var user = Users.FirstOrDefault(t => t.LoginName == LoginName || t.Email == LoginName || t.Phone == LoginName);
            if(user!=null)
            {
                return (new UserAccountModel()
                {
                    userId=user.Id,
                    LoginName=user.LoginName,
                    LoginPwd=user.LoginPwd,
                    PwdErrorCount=user.PwdErrorCount,
                    Enabled=user.Enabled
                });
            }
            return null;
        }
        public UserModel GetUserModelById(int userId)
        {
            var user = Users.FirstOrDefault(t => t.Id == userId);
            if (user != null)
            {
               return  Mapper.Map<UserModel>(user);
            }
            return null;
        }

        #endregion
    }
}
