using Domain.DB.Models;
using Domain.Site.Models;
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
    }
}
