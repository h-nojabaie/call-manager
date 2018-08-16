using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class Login
    {
        public int id { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public string username { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public string password { get; set; }
    }

}