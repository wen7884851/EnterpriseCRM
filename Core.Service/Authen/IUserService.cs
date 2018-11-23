using System.Linq;
using Domain.Site.Models;
using Domain.DB.Models;

namespace Core.Service.Authen
{
    public interface IUserService
    {
        #region 属性

        IQueryable<User> Users { get; }
        #endregion

        #region 公共方法
        UserAccountModel GetAccountByLoginName(string LoginName);

        UserModel GetUserModelById(int userId);
        UserModel GetUserModelByToken(string token);
        ActionResultViewModel CheckLogin(UserAccountModel user);
        #endregion
    }
}
