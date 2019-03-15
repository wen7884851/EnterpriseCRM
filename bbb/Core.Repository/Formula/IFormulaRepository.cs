using Framework.EFData;
using Domain.DB.Models;
using System;

namespace Domain.Repository
{
    public interface IFormulaRepository : IRepository<Formula, Int32>
    {
    }
}

