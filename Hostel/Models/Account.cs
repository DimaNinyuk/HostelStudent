using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hostel.Models
{
    public class LoginModel
    {
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Login { get; set; }
       
        //[DataType(DataType.Password), ErrorMessage = "Некорректный пароль"]
        public string Password { get; set; }

    }
}