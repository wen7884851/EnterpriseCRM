using Framework.EFData;
using Domain.DB.Models;
using System;
using System.ComponentModel.Composition;

namespace Domain.Repository.Impl
{
    [Export(typeof(IFormulaItemRepository))]
    public class FormulaItemRepository : EFRepositoryBase<FormulaItem, Int32>, IFormulaItemRepository
    {
    }
}

