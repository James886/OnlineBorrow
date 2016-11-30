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
   
    public class PrintController : Controller
    {
        //
        // GET: /Print/

        public ActionResult Index()
        {
            return View();
        }

       
        [Authorize]
        public ActionResult print(int information_id)
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
