using System.Linq;
using Core.Tool;
using Domain.DB.Models.System;
using Domain.Site.Common.Models;
using System.Collections.Generic;
using Domain.Site.Models.System.SystemSite;

namespace Business.Service.System
{
    /// <summary>
    /// 服务层接口 —— ISystemSiteService
    /// </summary>
    public interface ISystemSiteService
    {
        #region 属性

        IQueryable<SystemSite> SystemSites { get; }

        #endregion

        #region 公共方法
        /// <summary>
        /// 复选框数据源
        /// </summary>
        /// <returns></returns>
        List<KeyValueModel> GetKeyValueList();

        OperationResult Insert(SystemSiteModel model);

        OperationResult Update(SystemSiteModel model);

        /// <summary>
        ///  创建人： 谷文杰
        ///  日 期： 2018-25-04
        ///  描 述：认证系统
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>List&lt;KeyValueModel&gt;.</returns>
        List<KeyValueModel> GetKeyList(int id);

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        OperationResult Delete(int Id);

        #endregion
    }
}
