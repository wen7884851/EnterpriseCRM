using System;
using System.ComponentModel.Composition;
using System.Linq;

using Framework.EFData;
using Domain.DB.Models.Authen;


namespace Core.Repository.Authen.Impl
{
    [Export(typeof(IUserRoleRepository))]
    public class UserRoleRepository : EFRepositoryBase<UserRole, Int32>, IUserRoleRepository
    { }
}
