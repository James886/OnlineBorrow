using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBorrow1.Models
{
    public class Advice
    {
        [Key]
        public int advice_id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string title { get; set; }
        public string 内容 { get; set; }
        public DateTime time { get; set; }
    }
}