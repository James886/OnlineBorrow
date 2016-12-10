using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineBorrow1.Models;
using System.Web.Security;


namespace OnlineBorrow1.Controllers
{
    
    public class UserController : Controller
    {
        private UserContext db = new UserContext();
        //
        // GET: /User/

         
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

       [HttpPost]
        public ActionResult Login(User user, string returnUrl)
        {
            if (ValidateAdmin(user))
            {
                
                if (String.IsNullOrEmpty(returnUrl))
                {
                    FormsAuthentication.SetAuthCookie(user.username, false);
                    return RedirectToAction("inforCheck", "Check");
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }
            else if (ValidateMember(user))
            {
                if (String.IsNullOrEmpty(returnUrl))
                {
                    FormsAuthentication.SetAuthCookie(user.username, false);
                    return RedirectToAction("Index", "borrowInformation");
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }
            ModelState.AddModelError("", "您输入的账号或密码错误");
            return View(user);
        }
        private bool ValidateAdmin(User user)
        {
            UserContext userContext = new UserContext();
            User user_ = (from item in userContext.Users
                          where item.username == user.username && item.password == user.password && item.userCategory == 1
                          select item).FirstOrDefault();
            if (user_ == null)
                return false;
            HttpCookie user_idCookie = null;
            HttpCookie user_nameCookie = null;
            if (Request.Cookies["user_id"] != null)
            {

                user_idCookie = Request.Cookies["user_id"];
            }
            else
            {
                user_idCookie = new HttpCookie("user_id");
            }
            user_idCookie.Value = user_.user_id.ToString();
            Response.Cookies.Add(user_idCookie);
            if (Request.Cookies["user_name"] != null)
            {

                user_nameCookie = Request.Cookies["user_name"];
            }
            else
            {
                user_nameCookie = new HttpCookie("user_name");
            }
            user_nameCookie.Value = user_.username;
            Response.Cookies.Add(user_nameCookie);

            return true;
        }
        private bool ValidateMember(User user)
        {
            UserContext userContext = new UserContext();
            User user_ = (from item in userContext.Users
                          where item.username == user.username && item.password == user.password && item.userCategory != 1
                          select item).FirstOrDefault();
            if (user_ == null)
                return false;
            HttpCookie user_idCookie = null;
            HttpCookie user_nameCookie = null;
            if (Request.Cookies["user_id"] != null)
            {
                user_idCookie = Request.Cookies["user_id"];
            }
            else
            {
                user_idCookie = new HttpCookie("user_id");
            }
            user_idCookie.Value = user_.user_id.ToString();
            Response.Cookies.Add(user_idCookie);
            if (Request.Cookies["user_name"] != null)
            {

                user_nameCookie = Request.Cookies["user_name"];
            }
            else
            {
                user_nameCookie = new HttpCookie("user_name");
            }
            user_nameCookie.Value = user_.username;
            Response.Cookies.Add(user_nameCookie);
            HttpCookie user_categoryCookie = null;
            if (Request.Cookies["user_category"] != null)
            {
                user_categoryCookie = Request.Cookies["user_category"];
            }
            else
            {
                user_categoryCookie = new HttpCookie("user_category");
            }
            user_categoryCookie.Value = user_.userCategory.ToString();
            Response.Cookies.Add(user_categoryCookie);
            return true;  
        }
        public ActionResult passwordChanged()
        {
            return View();
            
        }
        public ActionResult Changed()
        {
            string password = Request["password"];
            string newPassword = Request["newPassword"];
            string confirmNewPassword = Request["confirmNewPassword"];


            HttpCookie CurrCookie = Request.Cookies["user_name"];
            string NUM = CurrCookie.Value;
            User user_ = (from item in db.Users
                          where item.username == NUM
                          select item
                              ).FirstOrDefault();
            if (user_.password == password && newPassword == confirmNewPassword)
            {
                user_.password = newPassword;
            }
            else
            {
                return RedirectToAction("passwordChanged", "User");
            }

            db.Entry<User>(user_).State = System.Data.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "borrowInformation");
            
        }

        public ActionResult LoginOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login", "User");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}