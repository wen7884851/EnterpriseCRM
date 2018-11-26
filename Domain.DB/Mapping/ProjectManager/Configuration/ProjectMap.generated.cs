using Framework.EFData;
using Domain.DB.Models;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;


namespace Domain.DB.Mapping
{
    internal partial class ProjectMap : EntityTypeConfiguration<Project>, IEntityMapper
    {
        public ProjectMap()
        {
            ProjectMapAppend();
        }
        partial void ProjectMapAppend();

        public void RegistTo(ConfigurationRegistrar configurations)
        {
            configurations.Add(this);
        }
    }
}
