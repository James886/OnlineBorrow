using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineBorrow1.Models;


namespace OnlineBorrow1.Controllers
{
    public class setWeekController : Controller
    {
        setWeekContext set_weekContext = new setWeekContext();
        //
        // GET: /setWeek/

        public ActionResult Index()
        {
            setWeek set_week = (from item in set_weekContext.set_week
                                where item.id == 1
                                select item).FirstOrDefault();
            return View(set_week);
        }

        public ActionResult Save()
        {
            setWeek set_week = (from item in set_weekContext.set_week
                                where item.id == 1
                                select item).FirstOrDefault();
            set_week.start_date = Convert.ToDateTime(Request["start_date"]);
            //setWeekContext set_weekContext = new setWeekContext();
            set_weekContext.Entry<setWeek>(set_week).State = System.Data.EntityState.Modified;
            set_weekContext.SaveChanges();
            return RedirectToAction("Index", "setWeek");
        }
    }
}
