﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Tool.Entity;

namespace Domain.DB.Models
{
    public class User : EntityBase<int>, ICreationAudited, IModificationAudited
    {
        public int? AddressId { get; set; }
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Enabled { get; set; }
        public int? PwdErrorCount { get; set; }
        public int? LoginCount { get; set; }
        public string Token { get; set; }
        public bool? IsRemeberUserName { get; set; }
        public DateTime? RegisterTime { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }
    }
}
