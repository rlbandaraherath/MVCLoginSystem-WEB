using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCLoginSystem.Models
{
    public class RecoverPassword
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mothers Maiden Name is required")]
        public string  security{ get; set; }

    }
}