using System;
using System.ComponentModel.Composition;
using System.Linq;

using Core.EFData;
using Domain.DB.Models.Authen;


namespace Domain.Repository.Authen.Impl
{
	/// <summary>
    /// 仓储操作层实现 —— Area
    /// </summary>
    [Export(typeof(IAreaRepository))]
    public class AreaRepository : EFRepositoryBase<Area, Int32>, IAreaRepository
    { }
}
