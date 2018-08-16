using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Collections.Generic;
using System.Data.Entity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdminViewCallsController : Controller
    {
        manageCallsEntities2 db = new manageCallsEntities2();
        string thisYear = (DateTime.Now.Year).ToString();// امسال
        string thisMonth = (DateTime.Now.Month).ToString();// ماه جاری
        string today = (DateTime.Now.Day).ToString();// امروز

        // GET: AdminViewCalls

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AcceptPage()
        {
            try
            {
                var unitNames = (from i in db.units select i.unitName).ToList();
                ViewBag.unitNames = unitNames;
                var roleNames = (from j in db.roles select j.roleName).ToList();
                ViewBag.roleNames = roleNames;
            }
            catch
            {
                return Content("<script language='javascript' type='text/javascript'>ERROR!!!!('Requested Successfully ');</script>");
            }
            return View();
        }

        [HttpPost]
        public ActionResult SetNewRole(string newRoleName, int _unitID)
        {
            try
            {
               
                //ارسال نام واحد های موجود
                var unitNames = db.units.ToList();
                ViewBag.unitNames = unitNames;
                //ارسال نام نقش های موجود
                var existRoleNames = db.roles.ToList();
                ViewBag.existRoleNames = existRoleNames;
               
                //اضافه کردن نقش جدید
                var addRole = ourMethodsClass.addNewRole(_unitID, newRoleName);
                if (addRole == false)
                {
                    var RoleNames = db.roles.ToList();
                    ViewBag.RoleNames = RoleNames;

                    ViewBag.messageResult = "!افزودن نقش جدید با خطا مواجه شد";
                }
                else
                {
                    var RoleNames = db.roles.ToList();
                    ViewBag.RoleNames = RoleNames;

                    ViewBag.messageResult = "!نقش جدید با موفقیت افزوده شد";
                }
            }
            catch (Exception ex)
            {
                return View("Index", new HandleErrorInfo(ex, "AdminViewCalls", "SetNewRole"));
            }

            return View(new role());
        }
        //----------------------گزارش برای یک ماه اخیر---------------------------//
        public ActionResult oneMonthReport()
        {
            try
            {
                var oneMonthRep = ourMethodsClass.date(1);
            }
            catch
            {
                string error = "خطا در انجام عملیات ";
                return View(error);
            }
            return View();
        }
        //----------------------گزارش برای سه ماه اخیر---------------------------//
        public ActionResult threeMonthReport()
        {
            try { var oneMonthRep = ourMethodsClass.date(3); }
            catch
            {
                string error = "خطا در انجام عملیات ";
                return View(error);
            } 
            return View();
        }
        //----------------------گزارش برای شش ماه اخیر---------------------------//
        public ActionResult sixMonthReport()
        {
            try { var oneMonthRep = ourMethodsClass.date(6); }
            catch
            {
                string error = "خطا در انجام عملیات ";
                return View(error);
            } 
            return View();
        }
        //----------------------گزارش برای یک سال اخیر---------------------------//
        public ActionResult oneYearReport()
        {
            try { var oneMonthRep = ourMethodsClass.date(12); }
            catch
            {
                string error = "خطا در انجام عملیات ";
                return View(error);
            } 
            return View();
        }
        //-----------------------گزارش گیری-----------------------------//
        [HttpPost]
        public ActionResult report(view_rawReports vr)
        {
            string lastName = vr.familyName;
            string number = vr.calledNumber;
            string chosenRole = vr.roleName;
            DateTime startDate = vr.callDate;
            DateTime finishDate = vr.callDate;
            TimeSpan? duration = vr.callDuration;

            var returnReport = ourMethodsClass.shownReport(lastName, number, chosenRole, startDate, finishDate, duration);

            return View(returnReport);
        
    }
    }
}