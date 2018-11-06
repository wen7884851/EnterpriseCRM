using System;
using System.ComponentModel.Composition;
using System.Linq;

using Domain.DB.Models.System;
using Core.Tool;
using Domain.Repository.System;
using Domain.Site.Models.System.Permission;
using System.Collections.Generic;
using Domain.Site.Models.System.RoleModulePermission;
using Domain.Site.Models.SSO;
using Domain.DB.Models;
using Domain.DB.Enum;

namespace Business.Service.System.Impl
{
	/// <summary>
    /// 服务层实现 —— RoleModulePermissionService
    /// </summary>
    [Export(typeof(IResourcesService))]
    public class ResourcesService : CoreServiceBase, IResourcesService
    {
        #region 属性

        [Import]
        public IResourcesRepository ResourcesRepository { get; set; }

        [Import]
        public ISystemSiteRepository SystemSiteRepository { get; set; }

        [Import]
        public IUserRepository UserRepository { get; set; }

        public IQueryable<Resources> Resourcess
        {
            get { return ResourcesRepository.Entities; }
        }

        public IQueryable<SystemSite> SystemSites
        {
            get { return SystemSiteRepository.Entities; }
        }

        public IQueryable<User> Users
        {
            get { return UserRepository.Entities; }
        }


        #endregion

        #region 公共方法

        public OperationResult SetResources( IEnumerable<ResourcesModel> addModulePermissionList, IEnumerable<ResourcesModel> removeModulePermissionList)
        {
            //逻辑删除
            if (removeModulePermissionList.Count() > 0)
            {
                foreach (var rmp in removeModulePermissionList)
                {
                    var updateEntity = Resourcess.FirstOrDefault(t =>t.ResourcesId== rmp.ResourcesId
                    &&t.ResourcesType == rmp.ResourcesType 
                    && t.ModuleId == rmp.ModuleId 
                    && t.PermissionId == rmp.PermissionId 
                    &&t.SystemId== rmp .SystemId&& t.IsDeleted == false);
                    if (updateEntity != null)
                    {
                        updateEntity.IsDeleted = true;
                        updateEntity.Modify();
                        ResourcesRepository.Update(updateEntity);
                    }
                }         
            }
            //插入 & 更新
            if (addModulePermissionList.Count() > 0)
            {
                foreach (var amp in addModulePermissionList)
                {
                    var updateEntity = Resourcess.FirstOrDefault(t => t.ResourcesId == amp.ResourcesId
                    && t.ResourcesType == amp.ResourcesType
                    && t.ModuleId == amp.ModuleId
                    && t.PermissionId == amp.PermissionId
                    && t.SystemId == amp.SystemId && t.IsDeleted == false);
                    if (updateEntity != null)
                    {
                        updateEntity.IsDeleted = false;
                        updateEntity.Modify();
                        ResourcesRepository.Update(updateEntity);
                    }
                    else 
                    {
                        var addEntity = new Resources
                        {
                            ResourcesId = amp.ResourcesId,
                            ResourcesType = amp.ResourcesType,
                            ModuleId = amp.ModuleId,
                            SystemId = amp.SystemId,
                            PermissionId = amp.PermissionId
                        };
                        addEntity.Create();
                        ResourcesRepository.Insert(addEntity);
                    }
                }
            }

			return new OperationResult(OperationResultType.Success, "授权成功");
        }

        public List<SubSystem> GetSubUser(User UserModel,int RType)
        {

            //var SystemSitemodel = SystemSites.FirstOrDefault(t => t.Gid == SystemGid && t.IsDeleted == false);

            var Resourcemodel = Resourcess.Where(t =>  t.ResourcesType == (int)ResourcesType.SystemForUser
             && t.ResourcesId == UserModel.Id && t.IsDeleted==false);

            if (Resourcemodel == null)
            {
                return null;
            }
            else
            {
                List<SubSystem> SubSystemModels = new List<SubSystem>();
                foreach (var r in Resourcemodel)
                {
                    var System = SystemSites.FirstOrDefault(t => t.Id == r.SystemId && t.IsDeleted == false);

                    int SubUserID = r.ResourcesId ;

                    var SubUser = UserModel;

                    var SubResource = Resourcess.FirstOrDefault(t => t.ResourcesType == (int)ResourcesType.SubUserForMainUser
                     && t.SystemId == System.Id && t.ResourcesId == r.ResourcesId && t.IsDeleted == false);

                    if(SubResource!=null&& SubResource.ModuleId!=null)
                    {
                        SubUserID = SubResource.ModuleId.Value;
                        SubUser = Users.FirstOrDefault(t => t.Id == SubUserID && t.IsDeleted == false);
                    }

                    SubSystemModels.Add(new SubSystem()
                    {
                        SystemGid= System.Gid,
                        SubUserID= SubUserID,
                        SubUserName= SubUser.LoginName
                    });

                }
                return SubSystemModels;
            }
        }
    
        public List<int> GetSystemUserIds(int SystemId)
        {
            var UserIds = Resourcess.Where(t => t.ResourcesType == (int)ResourcesType.SystemForUser &&t.SystemId==SystemId
            ).Select(t => t.ResourcesId).ToList();
            return UserIds;
        }

        #endregion
    }
}
