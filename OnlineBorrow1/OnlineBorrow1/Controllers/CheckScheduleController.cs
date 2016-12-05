using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineBorrow1.Models;
namespace OnlineBorrow1.Controllers
{

    public class CheckScheduleController : Controller
    {
        //
        // GET: /CheckSchedule/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CheckSchedule()
        {
            string datetime = Request["借用日期"];
            string weekstr = Convert.ToDateTime(datetime).DayOfWeek.ToString();
            string str1 = "2016-11-28";
            DateTime dt1 = Convert.ToDateTime(str1);  //这里你要显示几个信息？ 那你在哪里计算？
            DateTime dt2 = Convert.ToDateTime(datetime);  //为什么两个 dt, 基准应该是8月 那是第一周
            TimeSpan ts = dt2 - dt1;
            int a = Convert.ToInt32(ts.TotalDays);
            int weekCount = a / 7 + 15;

            HttpCookie weekIndexCookie = null;
            if (Request.Cookies["weekIndex"] != null)
            {
                weekIndexCookie = Request.Cookies["weekIndex"];
            }
            else
            {
                weekIndexCookie = new HttpCookie("weekIndex");
            }
            weekIndexCookie.Value = weekCount.ToString();
            Response.Cookies.Add(weekIndexCookie);


            String[] weekDay = new String[7] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            string index = (weekDay.ToList().IndexOf(weekstr) + 1).ToString();
            lookScheduleContext lookSchedulesContext = new lookScheduleContext();
            List<lookSchedule> lookSchedules = (from item in lookSchedulesContext.lookSchedules.ToList()
                                                where ("计" + item.实验室名 )== Request["借用机房"] && item.weekDay == index
                                                select item).ToList();
            string str = weekCount.ToString();

            if (lookSchedules.First().第一节 != null)
            {
                if (!lookSchedules.First().第一节.Contains(str))
                {
                    lookSchedules.First().第一节 = "null";
                }
            }


            if (lookSchedules.First().第二节 != null)
            {
                if (!lookSchedules.First().第二节.Contains(str))
                {
                    lookSchedules.First().第二节 = "null";
                }
            }

            if (lookSchedules.First().第三节 != null)
            {
                if (!lookSchedules.First().第三节.Contains(str))
                {
                    lookSchedules.First().第三节 = "null";
                }
            }

            if (lookSchedules.First().第四节 != null)
            {
                if (!lookSchedules.First().第四节.Contains(str))
                {
                    lookSchedules.First().第四节 = "null";
                }
            }

            if (lookSchedules.First().第五节 != null)
            {
                if (!lookSchedules.First().第五节.Contains(str))
                {
                    lookSchedules.First().第五节 = "null";
                }
            }

            if (lookSchedules.First().第六节 != null)
            {
                if (!lookSchedules.First().第六节.Contains(str))
                {
                    lookSchedules.First().第六节 = "null";
                }
            }

            if (lookSchedules.First().第七节 != null)
            {
                if (!lookSchedules.First().第七节.Contains(str))
                {
                    lookSchedules.First().第七节 = "null";
                }
            }

            if (lookSchedules.First().第八节 != null)
            {
                if (!lookSchedules.First().第八节.Contains(str))
                {
                    lookSchedules.First().第八节 = "null";
                }
            }
          
            return Json(new
            {

                cindex1 = lookSchedules.First().第一节,
                cindex2 = lookSchedules.First().第二节,
                cindex3 = lookSchedules.First().第三节,
                cindex4 = lookSchedules.First().第四节,
                cindex5 = lookSchedules.First().第五节,
                cindex6 = lookSchedules.First().第六节,
                cindex7 = lookSchedules.First().第七节,
                cindex8 = lookSchedules.First().第八节
            });

        }

    }
}
