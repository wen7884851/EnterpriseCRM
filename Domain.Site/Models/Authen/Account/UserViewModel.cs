using System;
using AutoMapper;
using Domain.Site.Models.Common;
using Domain.DB.Models;

namespace Domain.Site.Models
{
    public class UserViewModel : EntityCommon
    {
        public int Id { get; set; }
        public string LoginName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? RegisterTime { get; set; }
        public DateTime? LastLoginTime { get; set; }
    }

    public class SearchViewModel
    {
        public string LoginName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Enabled { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
        public string RegisteredStartdate { get; set; }
        public string RegisteredEnddate { get; set; }

    }

}
