using Framework.EFData;
using Domain.DB.Models;
using System;
using System.ComponentModel.Composition;

namespace Domain.Repository.Impl
{
    [Export(typeof(IFormulaRepository))]
    public class FormulaRepository : EFRepositoryBase<Formula, Int32>, IFormulaRepository
    {
    }
}

