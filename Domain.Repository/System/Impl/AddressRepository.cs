﻿using Core.EFData;
using Domain.DB.Models.System;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.System.Impl
{
    /// <summary>
    /// 仓储操作层实现 —— Area
    /// </summary>
    [Export(typeof(IAddressRepository))]
   public class AddressRepository : EFRepositoryBase<Address, Int32>, IAddressRepository
    {
    }
}
