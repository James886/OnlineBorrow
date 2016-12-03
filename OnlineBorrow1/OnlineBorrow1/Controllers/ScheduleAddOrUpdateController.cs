using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineBorrow1.Models;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Web.SessionState;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Core;
using System.Reflection;
using System.Diagnostics;
using System.Threading.Tasks;

namespace OnlineBorrow1.Controllers
{
    public class ScheduleAddOrUpdateController : Controller
    {
        lookScheduleContext db = new lookScheduleContext();
        //
        // GET: /ScheduleAddOrUpdate/

        [Authorize]
        public ActionResult Index()
        {
            

            return View(db.lookSchedules.ToList());

        }

        //
        // GET: /User/Details/5

        [Authorize]
        public ActionResult Details(int id = 0)
        {
            lookSchedule schedule = db.lookSchedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        //
        // GET: /User/Create
      
        public ActionResult Create()
        {
           
            return View();
           
        }
        
        //
        // POST: /User/Create

        
       
        public ActionResult Create(lookSchedule schedule)
        {



            if (ModelState.IsValid)
            {
                db.Entry(schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(schedule);
        }
       

        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            lookSchedule schedule = db.lookSchedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(lookSchedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schedule);
        }

        [Authorize]
        public ActionResult Delete(int id = 0)
        {

            lookSchedule schedule = db.lookSchedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            lookSchedule schedule = db.lookSchedules.Find(id);
            db.lookSchedules.Remove(schedule);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
