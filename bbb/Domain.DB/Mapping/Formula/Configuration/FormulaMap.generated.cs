using Framework.EFData;
using Domain.DB.Models;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;


namespace Domain.DB.Mapping
{
    internal partial class FormulaMap : EntityTypeConfiguration<Formula>, IEntityMapper
    {
        public FormulaMap()
        {
            FormulaMapAppend();
        }
        partial void FormulaMapAppend();

        public void RegistTo(ConfigurationRegistrar configurations)
        {
            configurations.Add(this);
        }
    }
}
