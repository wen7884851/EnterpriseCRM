using System;

using Core.EFData;
using Domain.DB.Models.System;


namespace Domain.Repository.System
{
	/// <summary>
    /// 仓储操作层接口 —— Area
    /// </summary>
    public interface IAreaRepository : IRepository<Area, Int32>
    { }
}
