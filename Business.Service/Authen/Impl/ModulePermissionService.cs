using System;
using System.ComponentModel.Composition;
using System.Linq;
using Domain.DB.Models.Authen;
using Framework.Tool;
using Core.Repository.Authen;
using Domain.Site.Models.Authen.Permission;
using System.Collections.Generic;


namespace Core.Service.Authen.Impl
{
	/// <summary>
    /// 服务层实现 —— ModulePermissionService
    /// </summary>
    [Export(typeof(IModulePermissionService))]
    public class ModulePermissionService : CoreServiceBase, IModulePermissionService
	{
		#region 属性

		[Import]
        public IModulePermissionRepository ModulePermissionRepository { get; set; }

        public IQueryable<ModulePermission> ModulePermissions
        {
            get { return ModulePermissionRepository.Entities; }
        }

		#endregion

		#region 公共方法

		public OperationResult SetButton(ButtonModel model)
        {
			#region Add & Update 
			var oldDataList = ModulePermissions.Where(t => t.ModuleId == model.ModuleId && t.IsDeleted == false).Select(t => t.PermissionId);
			var newDataList = model.SelectedButtonList.ToList();
			var intersectIds = oldDataList.Intersect(newDataList).ToList(); // Same Ids
			var updateIds = oldDataList.Except(intersectIds).ToList(); // Remove Ids
			var addIds = newDataList.Except(oldDataList).ToList(); // Add Ids
			//逻辑删除
			foreach (var removeId in updateIds)
			{
				var updateEntity = ModulePermissions.FirstOrDefault(t => t.ModuleId == model.ModuleId && t.PermissionId == removeId && t.IsDeleted == false);
				if (updateEntity != null)
				{
					updateEntity.IsDeleted = true;
					ModulePermissionRepository.Update(updateEntity);
				}
				
			}
			//插入 & 更新
			foreach (var addId in addIds)
			{
				var updateEntity = ModulePermissions.FirstOrDefault(t => t.ModuleId == model.ModuleId && t.PermissionId == addId && t.IsDeleted == true);
				if (updateEntity != null)
				{
					updateEntity.IsDeleted = false;
                    updateEntity.CreateTime = DateTime.Now;
                    ModulePermissionRepository.Update(updateEntity);
				}
				else
				{
					var addEntity = new ModulePermission { ModuleId = model.ModuleId, PermissionId = addId };
					ModulePermissionRepository.Insert(addEntity);
				}			
			}
			#endregion

            return new OperationResult(OperationResultType.Success, "设置成功");
		}

		#endregion
	}
}
