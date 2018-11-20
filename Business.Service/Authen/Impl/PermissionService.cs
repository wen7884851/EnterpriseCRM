using System;
using System.ComponentModel.Composition;
using System.Linq;

using Domain.DB.Models.Authen;
using Core.Repository.Authen;
using Domain.Site.Models.Authen.Permission;
using Framework.Tool;
using System.Collections.Generic;
using Domain.Site.Common.Models;

namespace Core.Service.Authen.Impl
{
	/// <summary>
    /// 服务层实现 —— PermissionService
    /// </summary>
    [Export(typeof(IPermissionService))]
    public class PermissionService : CoreServiceBase, IPermissionService
	{
		#region 属性

		[Import]
        public IPermissionRepository PermissionRepository { get; set; }

        public IQueryable<Permission> Permissions
        {
            get { return PermissionRepository.Entities; }
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
			var dataList = Permissions.Where(t => t.Enabled == true && t.IsDeleted == false)
								.Select(t=> new PermissionModel
								{
									Id= t.Id,
									Name = t.Name,
									OrderSort = t.OrderSort
								}).OrderBy(t => t.OrderSort).ToList();
			foreach (var data in dataList)
			{
				keyValueList.Add(new KeyValueModel { Text = data.Name, Value = data.Id.ToString() });
			}
			return keyValueList;
		}

        public OperationResult Insert(PermissionModel model)
        {
            var entity = new Permission
            {
                Code = model.Code,
                Icon = model.Icon,
                Name = model.Name,
                Description = model.Description,
                OrderSort = model.OrderSort,
                Enabled = model.Enabled
            };
            PermissionRepository.Insert(entity);
            return new OperationResult(OperationResultType.Success, "添加成功");
        }

        public OperationResult Update(PermissionModel model)
        {
            var entity = new Permission
            {
                Id = model.Id,
                Code = model.Code,
                Icon = model.Icon,
                Name = model.Name,
                Description = model.Description,
                OrderSort = model.OrderSort,
                Enabled = model.Enabled
            };
            PermissionRepository.Update(entity);
			return new OperationResult(OperationResultType.Success, "更新成功");
        }

        public OperationResult Delete(int Id)
        {
            var model = Permissions.FirstOrDefault(t => t.Id == Id);
            model.IsDeleted = true;

            PermissionRepository.Update(model);
            return new OperationResult(OperationResultType.Success, "删除成功");
		}

		#endregion
	}
}
