using Framework.EFData;
using Domain.DB.Models;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace Domain.DB.Mapping
{
    internal partial class ProjectPointMap : EntityTypeConfiguration<ProjectPoint>, IEntityMapper
    {
        public ProjectPointMap()
        {
            ProjectPointMapAppend();
        }
        partial void ProjectPointMapAppend();

        public void RegistTo(ConfigurationRegistrar configurations)
        {
            configurations.Add(this);
        }
    }
}
