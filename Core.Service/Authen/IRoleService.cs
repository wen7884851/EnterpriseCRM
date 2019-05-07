using Domain.DB.Models;
using Domain.Site.Models;
using Domain.Site.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IRoleService
    {
        #region 属性
        IQueryable<Role> Roles { get; }
        #endregion

        List<ModuleViewModel> GetMenuByRoleId(int roleId);
        List<OptionViewMode> GetAllRoleKeyValue();
        PageResult<RoleViewModel> GetRoleListByQuery(RoleQueryModel query);
    }
}
