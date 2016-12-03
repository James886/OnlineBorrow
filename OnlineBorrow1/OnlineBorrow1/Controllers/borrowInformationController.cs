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
    
    public class borrowInformationController : Controller
    {
        private borrowInformationContext db = new borrowInformationContext();

        //
        // GET: /borrowInformation/


         [Authorize] //是登录的action   当然不能再加这个   除登陆  其他action  哪里？
        public ActionResult Index()
        {

            return View();
        }

        //[HttpPost]  用于提交数据， 
        //默认是HttpGet 用于请求数据  不可能吧    这里是你请求的页面  必须是Get
        [Authorize]
        public ActionResult request(int pages = 1)
        {

            List<borrowInformation> borrowInformations = (from item in db.borrowInformations.ToList()
                                                          orderby item.提交时间 descending
                                                          select item).ToList();
            return View(borrowInformations);
        }

        [HttpPost] //这里没问题   
        public ActionResult SaveInformation()
        {

            HttpCookie CurrCookie = Request.Cookies["user_id"];
            int NUM = Convert.ToInt32(CurrCookie.Value);

            borrowInformation borrowInformations = new borrowInformation();
            borrowInformations.单位名称 = Request["单位名称"];
            borrowInformations.借用机房 = Request["借用机房"];
            borrowInformations.借用人 = Request["借用人"];
            borrowInformations.借用人身份 = Request["借用人身份"];
            borrowInformations.具体内容描述 = Request["具体内容描述"];
            borrowInformations.联系电话 = Request["联系电话"];
            borrowInformations.其它 = Request["其它"];
            borrowInformations.所在班级 = Request["所在班级"];
            borrowInformations.提交时间 = DateTime.Now;
            borrowInformations.批准时间 = DateTime.Now;
            borrowInformations.学号 = Request["学号"];
            borrowInformations.用途 = Request["用途"];
            borrowInformations.借用具体时间始 = Request["借用具体时间始"];
            borrowInformations.借用具体时间终 = Request["借用具体时间终"];
            borrowInformations.user_id = NUM;

            borrowInformationContext iContext = new borrowInformationContext();

            iContext.borrowInformations.Add(borrowInformations);
            iContext.SaveChanges();
            return RedirectToAction("checking", "borrowInformation");

        }

         [HttpPost]
        public ActionResult Update1()
        {
            HttpCookie CurrCookie = Request.Cookies["information_id"];
            int NUM = Convert.ToInt32(CurrCookie.Value);
            borrowInformation _borrowInformation = db.borrowInformations.SingleOrDefault(u => u.information_id == NUM);

            _borrowInformation.负责人意见 = Request["负责人意见"];
            _borrowInformation.系领导意见 = Request["系领导意见"];
            _borrowInformation.informationCategory = 1;

            db.Entry<borrowInformation>(_borrowInformation).State = System.Data.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("inforCheck", "Check");
        }

        [Authorize]
        public ActionResult requestCondition(int informationCategory = 1, int pages = 1)
        {
            int type_id = 0;
           // List<borrowInformation> borrowInformations = null;
            if (informationCategory == 0)
            {
                type_id = 0;
            }
            if (informationCategory == 1)
            {
                type_id = 1;
            }
            if (informationCategory == 2)
            {
                type_id = 2;
            }
            HttpCookie type_idCookie = null;
            if (Request.Cookies["type_id"] != null)
            {

                type_idCookie = Request.Cookies["type_id"];
            }
            else
            {
                type_idCookie = new HttpCookie("type_id");
            }
            type_idCookie.Value = type_id.ToString();
            Response.Cookies.Add(type_idCookie);

            IEnumerable<borrowInformation> borrowInformations = null;
            borrowInformationContext infortionsContext = new borrowInformationContext();
            HttpCookie CurrCookie = Request.Cookies["user_id"];
            int NUM = Convert.ToInt32(CurrCookie.Value);

            if (informationCategory == 2)
            {
                 borrowInformations = from items in infortionsContext.borrowInformations
                                                                    where items.user_id == NUM
                                                                    orderby items.提交时间 descending
                                                                    select items;
            }
            else
            {
                 borrowInformations = from items in infortionsContext.borrowInformations
                                                                    where items.informationCategory == informationCategory && items.user_id == NUM
                                                                    orderby items.提交时间 descending
                                                                    select items;
            }
            Paging paging = new Paging(borrowInformations,6,3,pages);
            return View(paging);

            /*
            if (informationCategory == 2)
            {
                borrowInformations = (from item in db.borrowInformations.ToList()
                                      where item.user_id == NUM
                                      orderby item.提交时间 descending
                                      select item).ToList();
            }
            else
            {
                borrowInformations = (from item in infortionsContext.borrowInformations.ToList()
                                      orderby item.提交时间 descending
                                      where item.informationCategory == informationCategory && item.user_id == NUM
                                      select item).ToList();
            }
             * */

           // return View(borrowInformations);
        }

      
        [Authorize]
        public ActionResult checking()
        {
            return View();
        }



        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}