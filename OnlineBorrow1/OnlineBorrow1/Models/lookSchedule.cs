using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBorrow1.Models
{
    public class lookSchedule
    {
        [Key]
        public int id { get; set; }
        public string 第一节 { get; set; }
        public string 第二节 { get; set; }
        public string 第三节 { get; set; }
        public string 第四节 { get; set; }
        public string 中午休息 { get; set; }
        public string 第五节 { get; set; }
        public string 第六节 { get; set; }
        public string 第七节 { get; set; }
        public string 第八节 { get; set; }
        public string 下午休息 { get; set; }
        public string 第九节 { get; set; }
        public string 第十节 { get; set; }
        public int  weekCategory { get; set; }
        public string weekDay { get; set; }
        public int useCategory { get; set; }
        public string 实验室名 { get; set; }
        

    }
}