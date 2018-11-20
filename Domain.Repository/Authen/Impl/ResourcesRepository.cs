using System;
using System.ComponentModel.Composition;
using System.Linq;

using Framework.EFData;
using Domain.DB.Models.Authen;


namespace Core.Repository.Authen.Impl
{
    [Export(typeof(IResourcesRepository))]
    public class ResourcesRepository : EFRepositoryBase<Resources, Int32>, IResourcesRepository
    { }
}
