using Framework.EFData;
using Domain.DB.Models;
using System;

namespace Domain.Repository
{
    public interface IFormulaItemRepository : IRepository<FormulaItem, Int32>
    {
    }
}

