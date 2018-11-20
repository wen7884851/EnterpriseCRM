using System;
using System.ComponentModel.Composition;
using System.Linq;

using Framework.EFData;
using Domain.DB.Models.Authen;


namespace Core.Repository.Authen.Impl
{
    [Export(typeof(ISystemSiteRepository))]
    public class SystemSiteRepository : EFRepositoryBase<SystemSite, Int32>, ISystemSiteRepository
    { }
}
