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
        public ActionResult Advice(int title = 1,int pages = 1)
        {

            int adviceCategory = 0;
            adviceCategory = title;
            HttpCookie adviceCategoryCookie = null;
            if (Request.Cookies["adviceCategory"] != null)
            {

                adviceCategoryCookie = Request.Cookies["adviceCategory"];
            }
            else
            {
                adviceCategoryCookie = new HttpCookie("adviceCategory");
            }
            adviceCategoryCookie.Value = adviceCategory.ToString();
            Response.Cookies.Add(adviceCategoryCookie);

            string title_str = title.ToString();
            AdviceContext adviceContext = new AdviceContext();
            IEnumerable<Advice> advices_ = from item in adviceContext.advices
                                           where item.title == title_str
                                           orderby item.time descending
                                           select item;
            Paging paging = new Paging(advices_,6,3,pages);
            return View(paging);
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
