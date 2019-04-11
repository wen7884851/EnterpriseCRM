using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Domain.Site.Models
{
    public class UserPhotoViewModel
    {
        public int UserId { get; set; }
        public HttpPostedFileBase File { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
