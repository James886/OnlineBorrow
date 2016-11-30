using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBorrow1.Models
{
    public class borrowInformation
    {
        [Key]
        public int information_id { get; set; }
        public string 单位名称 { get; set; }
        public string 借用人 { get; set; }
        public string 联系电话 { get; set; }
        public string 借用人身份 { get; set; }
        public string 所在班级 { get; set; }
        public string 学号 { get; set; }
        public string 用途 { get; set; }
        public string 其它 { get; set; }
        public string 具体内容描述 { get; set; }
        public string 借用机房 { get; set; }
        public string 借用具体时间始 { get; set; }
        public string 借用具体时间终 { get; set; }
        public DateTime 提交时间 { get; set; }
        public int informationCategory { get; set; }
        public string 负责人意见 { get; set; }
        public string 系领导意见 { get; set; }
        public DateTime 批准时间 { get; set; }
        public int user_id { get; set; }
    }

    
}
