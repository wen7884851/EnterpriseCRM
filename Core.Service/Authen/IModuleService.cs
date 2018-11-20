using System.Linq;

using Framework.Tool;
using Domain.DB.Models.Authen;
using Domain.Site.Models.Authen.Module;
using System.Collections.Generic;

namespace Core.Service.Authen
{
    /// <summary>
    /// 服务层接口 —— IModuleService
    /// </summary>
    public interface IModuleService
    {
        #region 属性

        IQueryable<Module> Modules { get; }
      
        IEnumerable<Module> GetModuleList();

        #endregion

        #region 公共方法

        OperationResult Insert(ModuleModel model);

        OperationResult Update(ModuleModel model);

        OperationResult Delete(int Id);

        #endregion
	}
}
