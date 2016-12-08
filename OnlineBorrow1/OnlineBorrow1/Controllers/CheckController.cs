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
            int type_id = 0;

            type_id = informationCategory;

            HttpCookie type_id_checkCookie = null;
            if (Request.Cookies["type_id_check"] != null)
            {

                type_id_checkCookie = Request.Cookies["type_id_check"];
            }
            else
            {
                type_id_checkCookie = new HttpCookie("type_id_check");
            }
            type_id_checkCookie.Value = type_id.ToString();
            Response.Cookies.Add(type_id_checkCookie);
           
            borrowInformationContext infortionsContext = new borrowInformationContext();
            IEnumerable<borrowInformation> borrowInformations = null;
            

            if (informationCategory == 2)
            {
                borrowInformations = from items in infortionsContext.borrowInformations
                                     orderby items.提交时间 descending
                                     select items;
            }
            else
            {
                borrowInformations = from items in infortionsContext.borrowInformations
                                     where items.informationCategory == informationCategory
                                     orderby items.提交时间 descending
                                     select items;
            }
            Paging paging = new Paging(borrowInformations, 11, 3, pages);
            return View(paging);

          
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
