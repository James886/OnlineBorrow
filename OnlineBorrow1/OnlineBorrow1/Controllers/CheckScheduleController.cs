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

            String[] weekDay = new String[7] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            string index = (weekDay.ToList().IndexOf(weekstr) + 1).ToString();
            lookScheduleContext lookSchedulesContext = new lookScheduleContext();
            List<lookSchedule> lookSchedules = (from item in lookSchedulesContext.lookSchedules.ToList()
                                                where item.实验室名 == Request["借用机房"] && item.weekDay == index
                                                select item).ToList();
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
