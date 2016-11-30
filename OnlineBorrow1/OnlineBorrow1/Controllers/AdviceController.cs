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
    
    public class AdviceController : Controller
    {
        //
        // GET: /Advice/

        [Authorize]
        public ActionResult Advice(int pages = 1)
        {

            AdviceContext adviceContext = new AdviceContext();
            List<Advice> advices = (from item in adviceContext.advices.ToList()
                                    orderby item.time descending
                                    select item).ToList();
            int a = advices.Count;
            a = (a / 10) + 1;
            List<Advice> advice = new List<Advice>();
            if (pages < a)
            {
                for (int i = (pages - 1) * 10; i < pages * 10; i++)
                {
                    advice.Add(advices[i]);
                }
            }
            if (pages == a)
            {
                for (int i = (pages - 1) * 10; i < advices.Count; i++)
                {
                    advice.Add(advices[i]);
                }
            }
            return View(advice);
        }


         [Authorize]
        public ActionResult SaveAdvice()
        {
            Advice advice = new Advice();
            advice.name = Request["name"];
            advice.phone = Request["phone"];
            advice.title = Request["title"];
            advice.内容 = Request["content"];
            advice.time = DateTime.Now;

            AdviceContext adviceContext = new AdviceContext();
            adviceContext.advices.Add(advice);
            adviceContext.SaveChanges();

            return RedirectToAction("Advice");
        }

    }
}
