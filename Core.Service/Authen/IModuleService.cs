﻿using Domain.DB.Models;
using Domain.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service.Authen
{
    public interface IModuleService
    {
        #region 属性
        IQueryable<Module> Modules { get; }
        #endregion

        List<OptionViewMode> GetLayerKeyValue();
        List<OptionViewMode> GetParentModuleKeyValue(int layer);
        ActionResultViewModel CreateModule(ModuleViewModel model);
    }
}