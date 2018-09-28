using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal_v1._0._1.Identity
{
    public class ApplicationRole:IdentityRole
    {
        public string Description { get; set; }
    }
}