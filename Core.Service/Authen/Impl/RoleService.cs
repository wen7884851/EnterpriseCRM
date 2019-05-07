﻿using AutoMapper;
using Core.Repository.Authen;
using Domain.DB.Models;
using Domain.Site.Models;
using Domain.Site.Models.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
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

        public PageResult<RoleViewModel> GetRoleListByQuery(RoleQueryModel query)
        {
            var expr = BuildSearchModules(query);
            var roleList = Mapper.Map<List<RoleViewModel>>(Roles.Where(expr).OrderBy(t=>t.CreateTime)
                .Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize));
            return new PageResult<RoleViewModel>()
            {
                Items = roleList,
                TotalItemsCount = Roles.Where(expr).Count()
            };
        }

        private Expression<Func<Role, bool>> BuildSearchModules(RoleQueryModel model)
        {
            var bulider = new DynamicLambda<Role>();
            Expression<Func<Role, bool>> expr = t => t.IsDeleted == false;
            Expression<Func<Role, bool>> tmp;
            if (!string.IsNullOrEmpty(model.Name))
            {
                tmp = t => t.Name.Contains(model.Name);
                expr = bulider.BuildQueryAnd(expr, tmp);
            }
            return expr;
        }
    }
}
