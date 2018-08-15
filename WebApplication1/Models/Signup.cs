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

    public class Signup
    {

        public int id { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public string username { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public string password { get; set; }
        [DisplayName("Confirm Password")]
        [Compare("password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public string familyName { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public string nationalityCode { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public string email { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public bool gender { get; set; }
    }
}