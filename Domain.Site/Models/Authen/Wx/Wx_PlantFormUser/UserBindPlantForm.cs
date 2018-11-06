//**********************************
//*ClassName:
//*Version:
//*Date:
//*Author:
//*Effect:
//**********************************

using Domain.Site.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Site.Models.Authen.Wx
{
    public class UserBindPlantForm : EntityCommon
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "用户ID错误")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "平台ID错误")]
        public int PlantFormId { get; set; }


    }
}
