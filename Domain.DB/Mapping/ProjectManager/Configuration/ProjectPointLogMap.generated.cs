using Framework.EFData;
using Domain.DB.Models;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace Domain.DB.Mapping
{
    internal partial class ProjectPointLogMap : EntityTypeConfiguration<PointLog>, IEntityMapper
    {
        public ProjectPointLogMap()
        {
            ProjectPointLogMapAppend();
        }
        partial void ProjectPointLogMapAppend();

        public void RegistTo(ConfigurationRegistrar configurations)
        {
            configurations.Add(this);
        }
    }
}
