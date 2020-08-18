using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLHoiThaoProject.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Vui lòng nhập user name!")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập password!")]
        public string passWord { get; set; }
    }
}