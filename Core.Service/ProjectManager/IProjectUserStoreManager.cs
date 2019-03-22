﻿using Domain.DB.Models;
using Domain.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IProjectUserStoreManager
    {
        IQueryable<ProjectPointUserStore> projectPointUserStores { get; }
        int[] GetUserStoreUserIdsByPointId(int pointId);
        int CreateProjectUserStore(ProjectUserStoreViewModel model);
        decimal GetPointOccupiedFundByPointId(int pointId);
        int UpdateProjectUserStore(ProjectUserStoreViewModel model);
        PageResult<ProjectUserStoreViewModel> GetUserStoreListByQuery(ProjectUserStoreQueryModel queryModel);
    }
}
