//**********************************
//*ClassName:
//*Version:
//*Date:
//*Author:
//*Effect:
//**********************************

using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DB.Mapping.Authen
{
    partial class AreaMap
    {
        partial void AreaMapAppend()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.AreaName)
                .HasMaxLength(50);
            this.Property(t => t.AreaCode)
              .HasMaxLength(50);


            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.AreaName).HasColumnName("AreaName");
            Property(t => t.AreaCode).HasColumnName("AreaCode");
            Property(t => t.CityID).HasColumnName("CityID");
            Property(t => t.Estate).HasColumnName("Estate");
            Property(t => t.Parent).HasColumnName("Parent");
            Property(t => t.Layer).HasColumnName("Layer");
            Property(t => t.CreateId).HasColumnName("CreateId");
            Property(t => t.CreateBy).HasColumnName("CreateBy");
            Property(t => t.CreateTime).HasColumnName("CreateTime");
            Property(t => t.ModifyId).HasColumnName("ModifyId");
            Property(t => t.ModifyBy).HasColumnName("ModifyBy");
            Property(t => t.ModifyTime).HasColumnName("ModifyTime");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            ToTable("Common_Auth_Area");
        }
    }
}
