using System;
using System.ComponentModel.Composition;
using System.Linq;

using Framework.EFData;
using Domain.DB.Models.Authen;


namespace Core.Repository.Authen.Impl
{
    [Export(typeof(IAreaRepository))]
    public class AreaRepository : EFRepositoryBase<Area, Int32>, IAreaRepository
    { }
}
