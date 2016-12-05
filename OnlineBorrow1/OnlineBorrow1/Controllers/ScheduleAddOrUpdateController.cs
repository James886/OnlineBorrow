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
        public ActionResult Index(int classCategory = 1)
        {
            List<lookSchedule> lookSchedules = (from item in db.lookSchedules.ToList()
                                                where item.weekCategory == classCategory
                                                select item).ToList();

            HttpCookie classCategoryCookie = null;
            if (Request.Cookies["classCategory"] != null)
            {
                classCategoryCookie = Request.Cookies["classCategory"];
            }
            else
            {
                classCategoryCookie = new HttpCookie("classCategory");
            }
            classCategoryCookie.Value = lookSchedules.First().实验室名;
            Response.Cookies.Add(classCategoryCookie);

            return View(lookSchedules);

        }

        //
        // GET: /User/Details/5

        [Authorize]
        public ActionResult ScheduleAddOrUpdate()
        {
            // HttpCookie CurrCookie = Request.Cookies["classCategory"];

            List<lookSchedule> lookSchedules = (from item in db.lookSchedules.ToList()
                                                where item.实验室名 == Request.Cookies["classCategory"].Value
                                                select item).ToList();
            int j = 1;
            for (int i = 0; i < lookSchedules.Count; i++, j++)
            {

                lookSchedules[i].第一节 = Request["第一大节" + j.ToString()];
                
                lookSchedules[i].第二节 = Request["第一大节" + j.ToString()];
               
                lookSchedules[i].第三节 = Request["第二大节" + j.ToString()];
                lookSchedules[i].第四节 = Request["第二大节" + j.ToString()];
                lookSchedules[i].第五节 = Request["第三大节" + j.ToString()];
                lookSchedules[i].第六节 = Request["第三大节" + j.ToString()];
                lookSchedules[i].第七节 = Request["第四大节" + j.ToString()];
                lookSchedules[i].第八节 = Request["第四大节" + j.ToString()];
                lookSchedules[i].第九节 = Request["第五大节" + j.ToString()];
                lookSchedules[i].第十节 = Request["第五大节" + j.ToString()];
               
            }


            lookScheduleContext lookContext = new lookScheduleContext();

            for (int i = 0; i < lookSchedules.Count; i++)
            {
                lookContext.Entry<lookSchedule>(lookSchedules[i]).State = System.Data.EntityState.Modified;
                //lookContext.lookSchedules.(lookSchedules[i]);
                lookContext.SaveChanges();
            }

            return RedirectToAction("Index", "ScheduleAddOrUpdate");
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
