﻿using Framework.EFData;
using Domain.DB.Models;
using System;

namespace Domain.Repository
{
    public interface IProjectTypeRepository : IRepository<ProjectType, int>
    {
    }
}