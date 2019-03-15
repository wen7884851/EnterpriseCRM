using Framework.EFData;
using Domain.DB.Models;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;


namespace Domain.DB.Mapping
{
    internal partial class ProjectPointUserStoreMap : EntityTypeConfiguration<ProjectPointUserStore>, IEntityMapper
    {
        public ProjectPointUserStoreMap()
        {
            ProjectPointUserStoreMapAppend();
        }
        partial void ProjectPointUserStoreMapAppend();

        public void RegistTo(ConfigurationRegistrar configurations)
        {
            configurations.Add(this);
        }
    }
}
