﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Tool.Entity;

namespace Domain.DB.Models.Authen
{
    public class User : EntityBase<int>, ICreationAudited, IModificationAudited
    {
        public User()
        {
			this.UserRole = new List<UserRole>();
        }

        public int AddressId { get; set; }
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }
        public string APPPassword { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Enabled { get; set; }
        public int PwdErrorCount { get; set; }
        public int LoginCount { get; set; }
        public int AreaID { get; set; }
        public bool IsCity { get; set; }
        public DateTime? RegisterTime { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }
        public bool IsLoginName { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
