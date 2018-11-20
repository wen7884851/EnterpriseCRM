using System;
using System.ComponentModel.Composition;
using System.Linq;

using Framework.Tool;
using Domain.DB.Models.Authen;
using Core.Repository.Authen;
using System.Collections.Generic;
using Domain.Site.Common.Models;
using Domain.Site.Models.Authen.RoleModulePermission;
using Domain.DB.Models;
using Framework.EFData.DBExtend;
using Domain.DB.Enum;

namespace Core.Service.Authen.Impl
{
	/// <summary>
    /// 服务层实现 —— RoleService
    /// </summary>
    [Export(typeof(IRoleService))]
    public class RoleService : CoreServiceBase, IRoleService
    {
        #region 属性

        [Import]
        public IRoleRepository RoleRepository { get; set; }
        [Import]
        public IResourcesRepository ResourcesRepository { get; set; }

        public IQueryable<Role> Roles
        {
            get { return RoleRepository.Entities; }
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 复选框数据源
        /// </summary>
        /// <returns></returns>
        public List<KeyValueModel> GetKeyValueList()
        {
            var keyValueList = new List<KeyValueModel>();
            var dataList = Roles.Where(t => t.Enabled == true && t.IsDeleted == false)
                                .OrderBy(t => t.OrderSort)
								.ToList();
            foreach (var data in dataList)
            {
                keyValueList.Add(new KeyValueModel { Text = data.Name, Value = data.Id.ToString() });
            }
            return keyValueList;
        }

        public OperationResult Delete(int Id)
        {
            var model = Roles.FirstOrDefault(t => t.Id == Id);
            model.IsDeleted = true;

            RoleRepository.Update(model);
            return new OperationResult(OperationResultType.Success, "删除成功");
        }

        #endregion
    }
}
