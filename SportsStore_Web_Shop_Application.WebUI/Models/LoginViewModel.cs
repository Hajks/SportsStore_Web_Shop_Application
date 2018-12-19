using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportsStore_Web_Shop_Application.WebUI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Prosze podac nazwe uzytkownika")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Prosze podac haslo")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}