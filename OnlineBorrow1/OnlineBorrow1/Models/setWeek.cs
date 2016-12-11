using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBorrow1.Models
{
    public class setWeek
    {
        [Key]
        public int id { get; set; }
        public DateTime start_date { get; set; }
        public string weekIndex { get; set; }
    }
}