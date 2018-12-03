using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace H2Service.Web.Models.Users
{
    public class LoginViewModel
    {
        [Display(Name ="输入工号")]
       // [Required(ErrorMessage ="工号必填")]
        public string UserNumber { get; set; }

        [Display(Name = "输入密码")]
       // [Required(ErrorMessage ="密码必填")]
        public string Password { get; set; }
    }
}