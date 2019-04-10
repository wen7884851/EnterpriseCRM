using System;
using System.ComponentModel.Composition;
using System.Linq;

using Framework.EFData;
using Domain.DB.Models;

namespace Core.Repository.Authen.Impl
{

    [Export(typeof(IModuleRepository))]
    public class ModuleRepository : EFRepositoryBase<Module, Int32>, IModuleRepository
    { }
}
