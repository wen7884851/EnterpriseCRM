using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DB.Mapping.Authen
{
    partial class UserProfileMap
    {
        /// <summary>
        /// 映射配置
        /// </summary>   		
        partial void UserProfileMapAppend()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.FullName)
                .HasMaxLength(50);

            this.Property(t => t.Education)
                .HasMaxLength(50);

            this.Property(t => t.Birthday)
                .HasMaxLength(30);

            this.Property(t => t.Age)
                .HasMaxLength(10);

            this.Property(t => t.CreateBy)
                .HasMaxLength(50);

            this.Property(t => t.ModifyBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Common_Auth_UserProfile");
            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.FullName).HasColumnName("FullName");
            this.Property(t => t.Aptitude).HasColumnName("Aptitude");
            this.Property(t => t.Education).HasColumnName("Education");
            this.Property(t => t.Birthday).HasColumnName("Birthday");
            this.Property(t => t.Age).HasColumnName("Age");
            this.Property(t => t.Sex).HasColumnName("Sex");
            this.Property(t => t.InductionDate).HasColumnName("InductionDate");
            this.Property(t => t.PositiveDate).HasColumnName("PositiveDate");
            this.Property(t => t.CreateId).HasColumnName("CreateId");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.ModifyId).HasColumnName("ModifyId");
            this.Property(t => t.ModifyBy).HasColumnName("ModifyBy");
            this.Property(t => t.ModifyTime).HasColumnName("ModifyTime");
        }
    }
}
