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
        UserAccountViewModel GetAccountByLoginName(string LoginName);

        UserViewModel GetUserModelById(int userId);
        UserViewModel GetUserModelByToken(string token);
        ActionResultViewModel CheckLogin(UserAccountViewModel user);
        #endregion
    }
}
