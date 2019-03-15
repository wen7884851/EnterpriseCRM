using System;
using System.ComponentModel.Composition;
using System.Linq;
using Framework.EFData;
using Domain.DB.Models;


namespace Core.Repository.Authen.Impl
{
    [Export(typeof(IUserRepository))]
    public class UserRepository : EFRepositoryBase<User, Int32>, IUserRepository
    { }
}
