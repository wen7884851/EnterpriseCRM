﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DB.Mapping
{
    partial class FormulaItemMap
    {
        partial void FormulaItemMapAppend()
        {
            // Primary Key
            this.Property(t => t.Id).HasColumnName("Id");
            Property(t => t.FormulaId).HasColumnName("FormulaId");
            Property(t => t.ItemName).HasColumnName("ItemName");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.ProjectPointId).HasColumnName("ProjectPointId");
            Property(t => t.CreateId).HasColumnName("CreateId");
            Property(t => t.CreateBy).HasColumnName("CreateBy");
            Property(t => t.CreateTime).HasColumnName("CreateTime");
            Property(t => t.ModifyId).HasColumnName("ModifyId");
            Property(t => t.ModifyBy).HasColumnName("ModifyBy");
            Property(t => t.ModifyTime).HasColumnName("ModifyTime");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            ToTable("Cms_Formula_Item");
        }
    }
}