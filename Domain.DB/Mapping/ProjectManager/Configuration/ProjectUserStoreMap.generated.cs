using Framework.EFData;
using Domain.DB.Models;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace Domain.DB.Mapping
{
    internal partial class ProjectUserStoreMap : EntityTypeConfiguration<ProjectUserStore>, IEntityMapper
    {
        public ProjectUserStoreMap()
        {
            ProjectUserStoreMapAppend();
        }
        partial void ProjectUserStoreMapAppend();

        public void RegistTo(ConfigurationRegistrar configurations)
        {
            configurations.Add(this);
        }
    }
}
