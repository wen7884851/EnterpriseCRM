using Framework.EFData;
using Domain.DB.Models;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;


namespace Domain.DB.Mapping
{
    internal partial class FormulaItemMap : EntityTypeConfiguration<FormulaItem>, IEntityMapper
    {
        public FormulaItemMap()
        {
            FormulaItemMapAppend();
        }
        partial void FormulaItemMapAppend();

        public void RegistTo(ConfigurationRegistrar configurations)
        {
            configurations.Add(this);
        }
    }
}
