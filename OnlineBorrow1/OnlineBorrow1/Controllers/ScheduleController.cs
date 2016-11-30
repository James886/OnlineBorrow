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
    
    public class ScheduleController : Controller
    {
        //
        // GET: /Schedule/

      
       [Authorize]
        public ActionResult schedule(int classId = 121)
        {

            scheduleContext schedulesContext = new scheduleContext();
            List<schedule> schedules = (from item in schedulesContext.schedules.ToList()
                                        where Convert.ToInt32(item.classId) == classId
                                        select item).ToList();

            return View(schedules);
        }

    }
}
