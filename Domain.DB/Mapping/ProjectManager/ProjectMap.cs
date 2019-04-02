using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DB.Mapping
{
    partial class ProjectMap
    {
        partial void ProjectMapAppend()
        {
            // Primary Key
            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.ProjectName).HasColumnName("ProjectName");
            Property(t => t.ProjectLeader).HasColumnName("ProjectLeader");
            Property(t => t.Content).HasColumnName("Content");
            Property(t => t.Address).HasColumnName("Address");
            Property(t => t.Note).HasColumnName("Note");
            Property(t => t.LinkPerson).HasColumnName("LinkPerson");
            Property(t => t.TotalCost).HasColumnName("TotalCost");
            Property(t => t.ContractMoney).HasColumnName("ContractMoney");
            Property(t => t.ManagementProportion).HasColumnName("ManagementProportion");
            Property(t => t.Managementer).HasColumnName("Managementer");
            Property(t => t.AuditProportion).HasColumnName("AuditProportion");
            Property(t => t.Auditer).HasColumnName("Auditer");
            Property(t => t.JudgementProportion).HasColumnName("JudgementProportion");
            Property(t => t.Judgementer).HasColumnName("Judgementer");
            Property(t => t.CommissionProportion).HasColumnName("CommissionProportion");
            Property(t => t.CreateId).HasColumnName("CreateId");
            Property(t => t.CreateBy).HasColumnName("CreateBy");
            Property(t => t.CreateTime).HasColumnName("CreateTime");
            Property(t => t.ModifyId).HasColumnName("ModifyId");
            Property(t => t.ModifyBy).HasColumnName("ModifyBy");
            Property(t => t.ModifyTime).HasColumnName("ModifyTime");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            ToTable("Cms_Project");
        }
    }
}
