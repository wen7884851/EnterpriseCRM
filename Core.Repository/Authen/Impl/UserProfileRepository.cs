using System;
using System.ComponentModel.Composition;
using System.Linq;
using Framework.EFData;
using Domain.DB.Models;


namespace Core.Repository.Authen.Impl
{
    [Export(typeof(IUserProfileRepository))]
    public class UserProfileRepository : EFRepositoryBase<UserProfile, Int32>, IUserProfileRepository
    { }
}
