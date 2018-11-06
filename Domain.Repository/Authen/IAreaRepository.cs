using System;

using Core.EFData;
using Domain.DB.Models.Authen;


namespace Domain.Repository.Authen
{
	/// <summary>
    /// 仓储操作层接口 —— Area
    /// </summary>
    public interface IAreaRepository : IRepository<Area, Int32>
    { }
}
