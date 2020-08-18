using System.ComponentModel.DataAnnotations;

namespace QLHoiThaoProject.Models
{
    public class TVLoginModel
    {
        [Key]
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập!")]
        public string userName { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu!")]
        public string passWord { get; set; }
    }
}