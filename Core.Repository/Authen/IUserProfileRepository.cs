using Framework.EFData;
using Domain.DB.Models;
using System;

namespace Core.Repository.Authen
{
    public interface IUserProfileRepository : IRepository<UserProfile, Int32>
    { }
}
