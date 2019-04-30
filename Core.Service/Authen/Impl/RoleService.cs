﻿using AutoMapper;
using Core.Repository.Authen;
using Domain.DB.Models;
using Domain.Site.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service.Authen.Impl
{
    [Export(typeof(IRoleService))]
    public class RoleService : CoreServiceBase, IRoleService
    {
        #region 属性
        [Import]
        public IRoleRepository _roleRepository { get; set; }

        public IQueryable<Role> Roles
        {
            get { return _roleRepository.NoCahecEntities; }
        }
        #endregion

        public List<ModuleViewModel> GetMenuByRoleId(int roleId)
        {
            var role = Roles.FirstOrDefault(t => t.Id == roleId && t.IsDeleted == false);
            if(role!=null)
            {
                var module = Mapper.Map<List<ModuleViewModel>>(role.RoleModules.Where(t=> t.IsDeleted == false).Select(t => t.Module));
                return module;
            }
            return null;
        }

        public List<OptionViewMode> GetAllRoleKeyValue()
        {
            var role = Roles.Where(t => t.IsDeleted == false).Select(t=>new OptionViewMode {
                key=t.Id,
                text=t.Name,
                value=t.Id
            }).ToList();
            return role;
        }
    }
}