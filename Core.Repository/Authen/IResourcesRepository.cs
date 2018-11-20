using System;

using Framework.EFData;
using Domain.DB.Models.Authen;


namespace Core.Repository.Authen
{
    public interface IResourcesRepository : IRepository<Resources, Int32>
    { }
}
