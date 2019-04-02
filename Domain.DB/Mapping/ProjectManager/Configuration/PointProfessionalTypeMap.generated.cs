using Framework.EFData;
using Domain.DB.Models;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;


namespace Domain.DB.Mapping
{
    internal partial class PointProfessionalTypeMap : EntityTypeConfiguration<PointProfessionalType>, IEntityMapper
    {
        public PointProfessionalTypeMap()
        {
            PointProfessionalTypeMapAppend();
        }
        partial void PointProfessionalTypeMapAppend();

        public void RegistTo(ConfigurationRegistrar configurations)
        {
            configurations.Add(this);
        }
    }
}
