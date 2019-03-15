using Framework.EFData;
using Domain.DB.Models;
using System;

namespace Domain.Repository
{
    public interface IProjectRepository : IRepository<Project, Int32>
    {
    }
}
