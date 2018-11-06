using Core.EFData;
using Domain.DB.Models.Authen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Authen
{
    /// <summary>
    /// 仓储操作层接口 —— Address
    /// </summary>
    public interface IAddressRepository : IRepository<Address, Int32>
    {
    }
}
