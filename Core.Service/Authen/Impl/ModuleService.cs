using System;
using System.ComponentModel.Composition;
using System.Linq;

using Framework.Tool;
using Domain.DB.Models.Authen;
using Core.Repository.Authen;
using Domain.Site.Models.Authen.Module;
using System.Collections.Generic;
using Framework.Common;

namespace Core.Service.Authen.Impl
{
	/// <summary>
    /// 服务层实现 —— ModuleService
    /// </summary>
    [Export(typeof(IModuleService))]
    public class ModuleService : CoreServiceBase, IModuleService
	{
      

        #region 属性

        [Import]
        public IModuleRepository ModuleRepository { get; set; }

        public IQueryable<Module> Modules
        {
            get { return ModuleRepository.Entities; }
        }

		#endregion

		#region 公共方法

		public OperationResult Insert(ModuleModel model)
        {
            var entity = new Module
            {
                Name = model.Name,
                Code = model.Code,
                ParentId = model.ParentId != 0 ? model.ParentId : null,
                LinkUrl = model.LinkUrl,
                Area = model.Area,
                Layer=model.Layer,
                Controller = model.Controller,
                Action = model.Action,
                OrderSort = model.OrderSort,
                Icon = model.Icon != null ? model.Icon : "",
                Enabled = model.Enabled,
                SystemID=model.SystemID,
                Sys_Gid=model.SystemGid,
                IsMenu=model.IsMenu
            };
            ModuleRepository.Insert(entity);
            CacheFactory.Cache().RemoveCache(CacheConfig.ModuleCacheKey);
            return new OperationResult(OperationResultType.Success, "添加成功");
        }

        public OperationResult Update(ModuleModel model)
        {
            var entity = new Module
            {
                Id = model.Id,
                Name = model.Name,
                Code = model.Code,
                ParentId = model.ParentId != 0 ? model.ParentId : null,
                Layer= model.Layer,
                LinkUrl = model.LinkUrl,
                Area = model.Area,
                Controller = model.Controller,
                Action = model.Action,
                OrderSort = model.OrderSort,
				Icon = model.Icon != null ? model.Icon : "",
                Enabled = model.Enabled,
                IsMenu=model.IsMenu,
                SystemID = model.SystemID,
                Sys_Gid = model.SystemGid
            };          
            ModuleRepository.Update(entity);
            CacheFactory.Cache().RemoveCache(CacheConfig.ModuleCacheKey);
            return new OperationResult(OperationResultType.Success, "更新成功");
        }

        public OperationResult Delete(int Id)
        {
			var model = Modules.FirstOrDefault(t => t.Id == Id);
			model.IsDeleted = true;

			ModuleRepository.Update(model);
            CacheFactory.Cache().RemoveCache(CacheConfig.ModuleCacheKey);
            return new OperationResult(OperationResultType.Success, "删除成功");
		}

        public IEnumerable<Module> GetModuleList()
        {
            return Modules.Where(t => t.IsDeleted == false && t.Enabled == true).ToList<Module>();
        }

        #endregion
    }
}
