using System;
using System.ComponentModel.Composition;
using System.Linq;

using Framework.EFData;
using Domain.DB.Models;


namespace Core.Repository.Authen.Impl
{
    [Export(typeof(IRoleRepository))]
    public class RoleRepository : EFRepositoryBase<Role, Int32>, IRoleRepository
    { }
}
