using System;
using System.Linq;

using Core.Tool;
using Domain.DB.Models.Authen;
using Domain.Site.Models.Authen.User;
using Domain.Site.Common.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Business.Service.Authen
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

        /// <summary>
        ///  创建人： 谷文杰
        ///  日 期： 2018-25-04
        ///  描 述：认证后台添加人员
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>OperationResult.</returns>
        OperationResult CertifiedInsert(UserModel model);
        OperationResult Update(User model);

        OperationResult Update(UpdateUserModel model);

        OperationResult Update(ChangePwdModel model);
        OperationResult UpdateUserInfo(UpdateUserModel model);

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        OperationResult Delete(UserModel model);
        /// <summary>
        ///  创建人： 谷文杰
        ///  日 期： 2018-25-04
        ///  描 述：修改App密码
        /// </summary>
        /// <param name="Model">The model.</param>
        /// <returns>OperationResult.</returns>
        OperationResult APPUpdate(User Model);

        #endregion
    }
}
