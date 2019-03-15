using Framework.EFData;
using Domain.DB.Models;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;


namespace Domain.DB.Mapping
{
    internal partial class ProjectTypeMap : EntityTypeConfiguration<ProjectType>, IEntityMapper
    {
        public ProjectTypeMap()
        {
            ProjectTypeMapAppend();
        }
        partial void ProjectTypeMapAppend();

        public void RegistTo(ConfigurationRegistrar configurations)
        {
            configurations.Add(this);
        }
    }
}
