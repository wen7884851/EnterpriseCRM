﻿using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Collections.Generic;

using Framework.Tool;
using Domain.DB.Models.Authen;
using Core.Repository.Authen;
using Domain.Site.Models;
using Domain.Site.Models.Authen.User;
using Framework.Common.SecurityHelper;
using Domain.Site.Common.Models;
using Domain.DB.Models;
using Framework.EFData.DBExtend;
using System.Web.Mvc;
using Framework.Common;
using Domain.DB.Enum;

namespace Core.Service.Authen.Impl
{
    /// <summary>
    /// 服务层实现 —— UserService
    /// </summary>
    [Export(typeof(IUserService))]
    public class UserService : CoreServiceBase, IUserService
    {
        #region 属性

        [Import]
        public IUserRepository UserRepository { get; set; }

        [Import]
        public IRoleRepository RoleRepository { get; set; }

        [Import]
        public IUserRoleRepository UserRoleRepository { get; set; }

        [Import]
        public IResourcesRepository ResourcesRepository { get; set; }

        [Import]
        public ISystemSiteRepository SystemSiteRepository { get; set; }
        [Import]
        public ICityRepository CityRepository { get; set; }
        [Import]
        public IAreaRepository AreaRepository { get; set; }
        public IQueryable<SystemSite> SystemSites
        {
            get { return SystemSiteRepository.Entities; }
        }

        public IQueryable<User> Users
        {
            get { return UserRepository.Entities; }
        }

        public IQueryable<Role> Roles
        {
            get { return RoleRepository.Entities; }
        }

        public IQueryable<UserRole> UserRoles
        {
            get { return UserRoleRepository.Entities; }
        }




        public IQueryable<Resources> Resourcess
        {
            get { return ResourcesRepository.Entities; }
        }
        #endregion

        #region 公共方法


        public OperationResult Insert(UserModel model)
        {
            UnitOfWork tran = new UnitOfWork();   //事务开启
            try
            {
                var entityUser = Users.Where(t => t.LoginName == model.LoginName).FirstOrDefault();
                if (entityUser == null)
                {
                    var entity = new User
                    {
                        LoginName = model.LoginName,
                        LoginPwd = DESProvider.EncryptString(model.NewLoginPwd),
                        FullName = model.FullName,
                        Email = model.Email,
                        Phone = model.Phone,
                        Enabled = model.Enabled,
                        PwdErrorCount = 0,
                        LoginCount = 0,
                        AreaID = model.AreaID,
                        IsCity = model.IsCity,
                        RegisterTime = DateTime.Now,

                    };

                    entity.Create();
                    #region Add User Role Mapping

                    foreach (int roleId in model.SelectedRoleList)
                    {
                        if (Roles.Any(t => t.Id == roleId))
                        {
                            entity.UserRole.Add(
                                new UserRole()
                                {
                                    User = entity,
                                    RoleId = roleId,
                                    CreateId = model.CreateId,
                                    CreateBy = model.CreateBy,
                                    CreateTime = DateTime.Now
                                });
                        }
                    }

                    #endregion

                    UserRepository.Insert(entity);

                    #region Add User Resources Mapping
                    List<Resources> resarr = new List<Resources>();
                    foreach (int SystemId in model.SelectedSystemList)
                    {
                        if (SystemSites.Any(t => t.Id == SystemId))
                        {
                            resarr.Add(new Resources()
                            {
                                ResourcesType = (int)ResourcesType.SystemForUser,
                                ResourcesId = entity.Id,
                                SystemId = SystemId,
                                CreateId = model.CreateId,
                                CreateBy = model.CreateBy,
                                CreateTime = DateTime.Now
                            });
                        }
                    }
                    ResourcesRepository.Insert(resarr);
                    #endregion
                    tran.Commit();
                    CacheFactory.Cache().RemoveCache(CacheConfig.UserRoleCacheKey);
                    return new OperationResult(OperationResultType.Success, "添加成功");
                }
                else
                {
                    return new OperationResult(OperationResultType.Error, "添加失败，用户名重复");
                }

            }
            finally
            {
                tran.Dispose();
            }
        }

        /// <summary>
        ///  创建人： 谷文杰
        ///  日 期： 2018-25-04
        ///  描 述：认证后台的添加
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>OperationResult.</returns>
        public OperationResult CertifiedInsert(UserModel model)
        {
            UnitOfWork tran = new UnitOfWork();   //事务开启
            try
            {
                var entityUser = Users.Where(t => t.LoginName == model.LoginName).FirstOrDefault();
                if (entityUser == null)
                {
                    var entity = new User
                    {
                        LoginName = model.LoginName,
                        LoginPwd = DESProvider.EncryptString(model.NewLoginPwd),
                        FullName = model.FullName,
                        APPPassword = model.NewLoginPwd,
                        Email = model.Email,
                        Phone = model.Phone,
                        Enabled = model.Enabled,
                        PwdErrorCount = 0,
                        LoginCount = 0,
                        AreaID = model.CityID,
                        AddressId = model.AreaID,
                        IsCity = model.IsCity,
                        RegisterTime = DateTime.Now,

                    };

                    entity.Create();
                    #region Add User Role Mapping

                    foreach (int roleId in model.SelectedRoleList)
                    {
                        if (Roles.Any(t => t.Id == roleId))
                        {
                            entity.UserRole.Add(
                                new UserRole()
                                {
                                    User = entity,
                                    RoleId = roleId,
                                    CreateId = model.CreateId,
                                    CreateBy = model.CreateBy,
                                    CreateTime = DateTime.Now
                                });
                        }
                    }

                    #endregion

                    UserRepository.Insert(entity);

                    #region Add User Resources Mapping
                    List<Resources> resarr = new List<Resources>();
                    foreach (int SystemId in model.SelectedSystemList)
                    {
                        if (SystemSites.Any(t => t.Id == SystemId))
                        {
                            resarr.Add(new Resources()
                            {
                                ResourcesType = (int)ResourcesType.SystemForUser,
                                ResourcesId = entity.Id,
                                SystemId = SystemId,
                                CreateId = model.CreateId,
                                CreateBy = model.CreateBy,
                                CreateTime = DateTime.Now
                            });
                        }
                    }
                    ResourcesRepository.Insert(resarr);
                    #endregion
                    tran.Commit();
                    CacheFactory.Cache().RemoveCache(CacheConfig.UserRoleCacheKey);
                    return new OperationResult(OperationResultType.Success, "添加成功");
                }
                else
                {
                    return new OperationResult(OperationResultType.Error, "添加失败，用户名重复");
                }

            }
            finally
            {
                tran.Dispose();
            }
        }
        /// <summary>
        /// 更新登录信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OperationResult Update(User model)
        {
            UserRepository.Update(model);
            return new OperationResult(OperationResultType.Success);
        }
        public OperationResult UpdateUserInfo(UpdateUserModel model)
        {
            var entity = Users.FirstOrDefault(t => t.Id == model.Id);

            entity.FullName = model.FullName;
            entity.Phone = model.Phone;
            entity.Enabled = model.Enabled;
            entity.Modify();
            UserRepository.Update(entity);
            return new OperationResult(OperationResultType.Success, "保存成功");
        }
        /// <summary>
        /// 更新基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OperationResult Update(UpdateUserModel model)
        {
            UnitOfWork tran = new UnitOfWork();
            try
            {
                var entity = Users.FirstOrDefault(t => t.Id == model.Id);

                entity.FullName = model.FullName;
                entity.Phone = model.Phone;
                entity.AreaID = model.CityID;
                entity.IsCity = model.IsCity;
                entity.Enabled = model.Enabled;
                entity.ModifyId = model.ModifyId;
                entity.ModifyBy = model.ModifyBy;
                entity.ModifyTime = DateTime.Now;

                #region Update User Role Mapping
                var oldRoleIds = entity.UserRole.Where(t => t.IsDeleted == false).Select(t => t.RoleId).ToList();
                var newRoleIds = model.SelectedRoleList.ToList();
                var intersectRoleIds = oldRoleIds.Intersect(newRoleIds).ToList(); // Same Ids
                var removeIds = oldRoleIds.Except(intersectRoleIds).ToList(); // Remove Ids
                var addIds = newRoleIds.Except(intersectRoleIds).ToList(); // Add Ids
                foreach (var removeId in removeIds)
                {
                    //更新状态
                    var userRole = UserRoles.FirstOrDefault(t => t.UserId == model.Id && t.RoleId == removeId);
                    userRole.IsDeleted = true;
                    userRole.Modify();

                    UserRoleRepository.Update(userRole);
                }
                foreach (var addId in addIds)
                {
                    var userRole = UserRoles.FirstOrDefault(t => t.UserId == model.Id && t.RoleId == addId);
                    // 已有该记录，更新状态
                    if (userRole != null)
                    {
                        userRole.IsDeleted = false;
                        userRole.Modify();
                        UserRoleRepository.Update(userRole);
                    }
                    // 插入
                    else
                    {
                        //entity.UserRole.Add(new UserRole
                        //{
                        //    UserId = model.Id,
                        //    RoleId = addId,
                        //    CreateId = model.CreateId,
                        //    CreateBy = model.CreateBy,
                        //    CreateTime = DateTime.Now
                        //});

                        var InUserRole = new UserRole
                        {
                            UserId = model.Id,
                            RoleId = addId
                        };
                        InUserRole.Create();
                        UserRoleRepository.Insert(InUserRole);


                    }
                }

                #endregion

                UserRepository.Update(entity);

                #region Update User Resources Mapping  
                var entityResources = Resourcess.Where(t => t.ResourcesId == model.Id && t.ResourcesType == (int)ResourcesType.SystemForUser);
                var oldSystemIds = entityResources.Where(t => t.IsDeleted == false).Select(t => t.SystemId).ToList();
                var newSystemIds = model.SelectedSystemList.ToList();
                var intersectSystemIds = oldSystemIds.Intersect(newSystemIds).ToList(); // Same Ids
                var removeResourcesIds = oldSystemIds.Except(intersectSystemIds).ToList(); // Remove Ids
                var addResourcesIds = newSystemIds.Except(intersectSystemIds).ToList(); // Add Ids

                foreach (var removeId in removeResourcesIds)
                {
                    //更新状态
                    var Resource = Resourcess.FirstOrDefault(t => t.ResourcesId == model.Id && t.SystemId == removeId && t.ResourcesType == (int)ResourcesType.SystemForUser);
                    Resource.IsDeleted = true;
                    Resource.Modify();

                    ResourcesRepository.Update(Resource);
                }

                foreach (var addId in addResourcesIds)
                {
                    var Resource = Resourcess.FirstOrDefault(t => t.ResourcesId == model.Id && t.SystemId == addId && t.ResourcesType == (int)ResourcesType.SystemForUser);
                    // 已有该记录，更新状态
                    if (Resource != null)
                    {
                        Resource.IsDeleted = false;
                        Resource.Modify();
                        ResourcesRepository.Update(Resource);
                    }
                    // 插入
                    else
                    {
                        var NewResources = new Resources()
                        {
                            ResourcesId = model.Id,
                            ResourcesType = (int)ResourcesType.SystemForUser,
                            SystemId = addId
                        };
                        NewResources.Create();
                        ResourcesRepository.Insert(NewResources);
                    }
                }
                #endregion

                tran.Commit();
                CacheFactory.Cache().RemoveCache(CacheConfig.UserRoleCacheKey);
                return new OperationResult(OperationResultType.Success, "添加成功");
            }
            finally
            {
                tran.Dispose();
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OperationResult Update(ChangePwdModel model)
        {
            var entity = Users.FirstOrDefault(t => t.Id == model.Id);
            entity.LoginPwd = MD5Provider.GetMD5String(model.NewLoginPwd);
            entity.Modify();


            UserRepository.Update(entity);
            return new OperationResult(OperationResultType.Success, "修改密码成功");
        }

        public OperationResult Delete(UserModel model)
        {
            var entity = Users.FirstOrDefault(t => t.Id == model.Id);
            entity.IsDeleted = true;
            entity.Modify();

            UserRepository.Update(entity);
            return new OperationResult(OperationResultType.Success, "删除成功");
        }

        public List<KeyValueModel> GetKeyValueList(int SystemID)
        {
            var keyValueList = new List<KeyValueModel>();
            var UserInfo = Resourcess.Where(m => m.ResourcesType == (int)ResourcesType.SystemForUser & m.SystemId == SystemID & m.IsDeleted == false)
            .Select(s => s.ResourcesId).ToList();
            var dataList = Users.Where(t => t.Enabled == true && t.IsDeleted == false && UserInfo.Contains(t.Id))
                                .OrderBy(t => t.Id)
                                .ToList();
            foreach (var data in dataList)
            {
                keyValueList.Add(new KeyValueModel { Text = data.FullName, Value = data.Id.ToString() });
            }
            return keyValueList;
        }

        /// <summary>
        /// 创建人： 谷文杰
        /// 日 期： 2018-25-04
        /// 描 述：修改App密码
        /// </summary>
        /// <param name="Model">The model.</param>
        /// <returns>OperationResult.</returns>
        public OperationResult APPUpdate(User Model)
        {
            try
            {
                Model.ModifyId = Model.Id;
                Model.ModifyBy = Model.LoginName;
                Model.ModifyTime = DateTime.Now;
                UserRepository.Update(Model);
                return new OperationResult(OperationResultType.Success, "修改密码成功");
            }
            catch (Exception)
            {
                return new OperationResult(OperationResultType.Error, "修改密码错误");
                throw;
            }
        }

        #endregion
    }
}