using Portal_v1._0._1.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Portal_v1._0._1.Models
{
    public class CVModel
    {
        public int Id { get; set; }
        [Required]
        public string FileName { get; set; }

        [Required]
        public string FilePath { get; set; }

        public string PortalUserId { get; set; }
        public PortalUser User { get; set; }
    }
    public class CvGelen
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}