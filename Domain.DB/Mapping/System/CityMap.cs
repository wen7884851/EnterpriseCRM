//**********************************
//*ClassName:
//*Version:
//*Date:
//*Author:
//*Effect:
//**********************************

using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DB.Mapping.System
{
    partial class CityMap
    {
        partial void CityMapAppend()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.CityName)
                .HasMaxLength(30);

            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Provice).HasColumnName("Provice");
            Property(t => t.CityName).HasColumnName("CityName");
            Property(t => t.CreateId).HasColumnName("CreateId");
            Property(t => t.CreateBy).HasColumnName("CreateBy");
            Property(t => t.CreateTime).HasColumnName("CreateTime");
            Property(t => t.ModifyId).HasColumnName("ModifyId");
            Property(t => t.ModifyBy).HasColumnName("ModifyBy");
            Property(t => t.ModifyTime).HasColumnName("ModifyTime");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            ToTable("Common_Auth_City");

            //表的引用关系映射
            HasMany(e => e.Areas).WithOptional(e => e.City).HasForeignKey(e => e.CityID);
        }
    }
}
