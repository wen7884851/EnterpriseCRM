using Framework.EFData;
using Domain.DB.Models;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;


namespace Domain.DB.Mapping
{
    internal partial class ProjectFragmentMap : EntityTypeConfiguration<ProjectFragment>, IEntityMapper
    {
        public ProjectFragmentMap()
        {
            ProjectFragmentMapAppend();
        }
        partial void ProjectFragmentMapAppend();

        public void RegistTo(ConfigurationRegistrar configurations)
        {
            configurations.Add(this);
        }
    }
}
