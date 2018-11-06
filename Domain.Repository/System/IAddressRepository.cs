using Core.EFData;
using Domain.DB.Models.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.System
{
    /// <summary>
    /// 仓储操作层接口 —— Address
    /// </summary>
    public interface IAddressRepository : IRepository<Address, Int32>
    {
    }
}
