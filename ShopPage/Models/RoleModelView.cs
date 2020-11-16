using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ShopPage
{
    public class RoleModelView
    {
        public string RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}