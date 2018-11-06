using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models.System.FaceLiveness
{
   public class ImagesModel
    {
        public int id { get; set; }
        public int PsId { get; set; }
        [Display(Name = "图片名称")]
        public string FileName { get; set; }
        [Display(Name = "图片格式")]
        public string FileExtension { get; set; }
        [Display(Name = "图片路径")]
        public string FileUrl { get; set; }
    }
}
