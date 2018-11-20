﻿using System;
using System.ComponentModel.Composition;
using System.Linq;

using Framework.EFData;
using Domain.DB.Models.Authen;


namespace Core.Repository.Authen.Impl
{
    [Export(typeof(IModulePermissionRepository))]
    public class ModulePermissionRepository : EFRepositoryBase<ModulePermission, Int32>, IModulePermissionRepository
    { }
}