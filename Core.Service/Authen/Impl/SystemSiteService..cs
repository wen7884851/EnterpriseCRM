using System;
using System.ComponentModel.Composition;
using System.Linq;

using Domain.DB.Models.Authen;
using Framework.Tool;
using Core.Repository.Authen;
using Domain.Site.Models.Authen.Permission;
using System.Collections.Generic;
using Domain.Site.Models.Authen.SystemSite;
using Domain.Site.Common.Models;

namespace Core.Service.Authen.Impl
{
    /// <summary>
    /// 服务层实现 —— SystemSiteService
    /// </summary>
    [Export(typeof(ISystemSiteService))]
    public class SystemSiteService : CoreServiceBase, ISystemSiteService
    {
        #region 属性

        [Import]
        public ISystemSiteRepository SystemSiteRepository { get; set; }

        public IQueryable<SystemSite> SystemSites
        {
            get { return SystemSiteRepository.Entities; }
        }

        public OperationResult Delete(int Id)
        {
            var model = SystemSites.FirstOrDefault(t => t.Id == Id);
            model.IsDeleted = true;
            model.Modify();
            SystemSiteRepository.Update(model);
            return new OperationResult(OperationResultType.Success, "删除成功");
        }

        public List<KeyValueModel> GetKeyValueList()
        {
            var keyValueList = new List<KeyValueModel>();
            var dataList = SystemSites.Where(t => t.IsDeleted == false)
                                .OrderBy(t => t.Id)
                                .ToList();
            foreach (var data in dataList)
            {
                keyValueList.Add(new KeyValueModel { Text = data.SystemName, Value = data.Id.ToString() });
            }
            return keyValueList;
        }

        /// <summary>
        ///  创建人： 谷文杰
        ///  日 期： 2018-25-04
        ///  描 述：认证后台系统
        /// </summary>
        /// <param name="id">SystemSites ID</param>
        /// <returns>List&lt;KeyValueModel&gt;.</returns>
        public List<KeyValueModel> GetKeyList(int id)
        {
            var keyValueList = new List<KeyValueModel>();
            var dataList = SystemSites.Where(t => t.IsDeleted == false && t.Id == id)
                                .OrderBy(t => t.Id)
                                .ToList();
            foreach (var data in dataList)
            {
                keyValueList.Add(new KeyValueModel { Text = data.SystemName, Value = data.Id.ToString() });
            }
            return keyValueList;
        }


        public OperationResult Insert(SystemSiteModel model)
        {
            SystemSite system = new SystemSite()
            {
                SystemName = model.SystemName,
                Url = model.Url,
                Description = model.Description,
                Icon = model.Icon,
                Code = model.Code,
                Gid = System.Guid.NewGuid().ToString()
            };
            system.Create();
            SystemSiteRepository.Insert(system);
            return new OperationResult(OperationResultType.Success, "添加成功");
        }

        public OperationResult Update(SystemSiteModel model)
        {
            var system = SystemSites.Where(t => t.Id == model.Id).FirstOrDefault();
            if (system != null)
            {
                system.SystemName = model.SystemName;
                system.Url = model.Url;
                system.Description = model.Description;
                system.Icon = model.Icon;
                system.Code = model.Code;
                system.Modify();
                SystemSiteRepository.Update(system);
            }
            else
            {
                Insert(model);
            }
            return new OperationResult(OperationResultType.Success, "更新成功");
        }

        #endregion
    }
}
