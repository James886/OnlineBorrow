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
    public class CheckController : Controller
    {
        //
        // GET: /Check/

        public ActionResult Index()
        {
            return View();
        }

        
        [Authorize]
        public ActionResult inforCheck(int informationCategory = 0, int pages = 1)
        {
            List<borrowInformation> borrowInformations = null;
            borrowInformationContext infortionsContext = new borrowInformationContext();
            List<borrowInformation> borrowInformation1 = new List<Models.borrowInformation>();
            if (informationCategory == 2)
            {

                borrowInformations = (from item in infortionsContext.borrowInformations.ToList()
                                      orderby item.提交时间 descending
                                      select item).ToList();
                int a = borrowInformations.Count;
                a = (a / 10) + 1;
                if (pages < a)
                {
                    for (int i = (pages - 1) * 10; i < pages * 10; i++)
                    {
                        borrowInformation1.Add(borrowInformations[i]);
                    }
                }
                if (pages == a)
                {
                    for (int i = (pages - 1) * 10; i < borrowInformations.Count; i++)
                    {
                        borrowInformation1.Add(borrowInformations[i]);
                    }
                }
            }

            else
            {
                borrowInformations = (from item in infortionsContext.borrowInformations.ToList()
                                      orderby item.提交时间 descending
                                      where item.informationCategory == informationCategory
                                      select item).ToList();

                int a = borrowInformations.Count;
                a = (a / 10) + 1;

                if (pages < a)
                {
                    for (int i = (pages - 1) * 10; i < pages * 10; i++)
                    {
                        borrowInformation1.Add(borrowInformations[i]);
                    }
                }
                if (pages == a)
                {
                    for (int i = (pages - 1) * 10; i < borrowInformations.Count; i++)
                    {
                        borrowInformation1.Add(borrowInformations[i]);
                    }
                }
            }
            return View(borrowInformation1);
        }

        
        [Authorize]
        public ActionResult detailCheck(int information_id)
        {

            HttpCookie CategoryCookie = null;

            if (Request.Cookies["information_id"] != null)
            {
                CategoryCookie = Request.Cookies["information_id"];
            }
            else
            {
                CategoryCookie = new HttpCookie("information_id");
            }

            CategoryCookie.Value = information_id.ToString();
            Response.Cookies.Add(CategoryCookie);

            borrowInformationContext infortionsContext = new borrowInformationContext();
            HttpCookie CurrCookie = Request.Cookies["information_id"];
            int NUM = Convert.ToInt32(CurrCookie.Value);
            List<borrowInformation> borrowInformations = (from item in infortionsContext.borrowInformations.ToList()
                                                          where item.information_id == NUM
                                                          select item).ToList();
            return View(borrowInformations);

        }

    }
}
