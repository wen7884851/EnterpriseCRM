
using System;
using System.ComponentModel.Composition;
using System.Linq;

using Framework.EFData;
using Domain.DB.Models.Authen;


namespace Core.Repository.Authen.Impl
{
    [Export(typeof(IPermissionRepository))]
    public class PermissionRepository : EFRepositoryBase<Permission, Int32>, IPermissionRepository
    { }
}
