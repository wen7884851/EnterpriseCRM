using System;

using Framework.EFData;
using Domain.DB.Models;


namespace Core.Repository.Authen
{
    public interface IUserRepository : IRepository<User, Int32>
    { }
}
