using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineBorrow1.Models;
using System.Timers;

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
            string datetime = "";
            if (Request["借用日期"] == null)
            {
                datetime = DateTime.Now.ToLongDateString();
            }
            else
            {
                datetime = Request["借用日期"];
            }
            string weekstr = Convert.ToDateTime(datetime).DayOfWeek.ToString();
            string str1 = "2016-08-22";
            DateTime dt1 = Convert.ToDateTime(str1);  //这里你要显示几个信息？ 那你在哪里计算？
            DateTime dt2 = Convert.ToDateTime(datetime);  //为什么两个 dt, 基准应该是8月 那是第一周
            TimeSpan ts = dt2 - dt1;
            int a = Convert.ToInt32(ts.TotalDays);
            int weekCount = a / 7 + 1;
            DateTime dt = DateTime.Now;
            //Request.Cookies["weekIndex"].Expires = dt.Second.ToString() + "7";

            String[] weekDay = new String[7] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            string index = (weekDay.ToList().IndexOf(weekstr) + 1).ToString();
            lookScheduleContext lookSchedulesContext = new lookScheduleContext();
            lookSchedule lookSchedules = (from item in lookSchedulesContext.lookSchedules.ToList()
                                                where ("计" + item.实验室名 )== Request["借用机房"] && item.weekDay == index
                                                select item).FirstOrDefault();
            borrowInformationContext borrowInformationsContext = new borrowInformationContext();
            List<borrowInformation> borrowInformation = (from item in borrowInformationsContext.borrowInformations.ToList()
                                                       where item.借用机房 == Request["借用机房"] && item.借用具体时间始 == Request["借用日期"]
                                                       select item).ToList();
            int count = borrowInformation.Count();
            string[] str2 = new string[] {"", lookSchedules.第一节, lookSchedules.第二节, lookSchedules.第三节, lookSchedules.第四节, lookSchedules.第五节, lookSchedules.第六节, lookSchedules.第七节, lookSchedules.第八节 };
            string str = weekCount.ToString();
            for (int i = 1; i < 9; i++)
            {
                if (str2[i] != null)
                {
                    if (!str2[i].Contains(str))
                    {
                        if (count == 0)
                        {
                            str2[i] = null;
                        }
                        else
                        {
                            for (int j = 0; j < count; j++)
                            {
                                if (borrowInformation[j].借用具体时间终.Contains(i.ToString()))
                                {
                                    str2[i] = "已借用";
                                }
                                else
                                {
                                    str2[i] = null;
                                }
                            }
                        }
                            
                    }
                    else
                    {
                        str2[i] = "有课";
                    }
                }
                else
                {
                    for (int j = 0; j < count; j++)
                    {
                        if (borrowInformation[j].借用具体时间终.Contains(i.ToString()))
                        {
                            str2[i] = "已借用";
                        }
                    }
                }

            }

           
            return Json(new
            {

                weekindex = str,
                cindex1 = str2[1],
                cindex2 = str2[2],
                cindex3 = str2[3],
                cindex4 = str2[4],
                cindex5 = str2[5],
                cindex6 = str2[6],
                cindex7 = str2[7],
                cindex8 = str2[8]
            });

        }

    }
}
