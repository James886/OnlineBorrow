using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBorrow1.Models
{
    public class schedule
    {
        [Key]
        public int id { get; set; }
        public string classId { get; set; }
        public string imgpath { get; set; }

    }
}