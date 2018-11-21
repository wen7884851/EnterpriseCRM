using System.Linq;
using Framework.Tool;
using Domain.DB.Models.Authen;
using Domain.Site.Models.Authen.User;
using Domain.Site.Common.Models;
using System.Collections.Generic;

namespace Core.Service.Authen
{
    /// <summary>
    /// 服务层接口 —— IUserService
    /// </summary>
    public interface IUserService
    {
        #region 属性

        IQueryable<User> Users { get; }
        #endregion

        #region 公共方法
        /// <summary>
        /// 复选框数据源
        /// </summary>
        /// <returns></returns>
        List<KeyValueModel> GetKeyValueList(int SystemID);
        OperationResult Insert(UserModel model);
        OperationResult CertifiedInsert(UserModel model);
        OperationResult Update(User model);

        OperationResult Update(UpdateUserModel model);

        OperationResult Update(ChangePwdModel model);
        OperationResult UpdateUserInfo(UpdateUserModel model);

        OperationResult Delete(UserModel model);
        OperationResult APPUpdate(User Model);

        #endregion
    }
}
