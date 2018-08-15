using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    public class ourMethodsClass
    {

        //بعنوان ورودی بازه ی ماه را میگیرد
        //در نهایت این متد تاریخ های بین بازه ی مشخص شده را برمیگرداند
        public static object date(int monDuration)
        {
            manageCallsEntities2 db = new manageCallsEntities2();
            string thisYear = (DateTime.Now.Year).ToString();// امسال
            string thisMonth = (DateTime.Now.Month).ToString();// ماه جاری
            string today = (DateTime.Now.Day).ToString();// امروز

            var todayDate = Convert.ToInt32(thisYear + thisMonth + today);//تاریخ امروز

            var lastMon = (Convert.ToInt16(thisMonth) - monDuration);//ماه گذشته
            var lastYear = thisYear;

            if (lastMon == 0)
            {
                lastMon = 12;
                lastYear = (Convert.ToInt16(thisYear) - monDuration).ToString();//سال گذشته 
            }

            var lastMonDate = Convert.ToInt32(lastYear + lastMon + today);//تاریخ بازه ی انتخابی 

            //لیست گزارش تماس تمام افراد در بازه ی مشخص شده
            var report = from d in db.calls
                         where d.callDate.Month >= lastMonDate
                         select d;
            return report;

        }
        //تبدیل تاریخ میلادی به شمسی
        public static string miladitoshamsi(int yy, int mm, int dd, int halat)
        {

            DateTime date = new DateTime(yy, mm, dd);
            System.Globalization.PersianCalendar pdate = new System.Globalization.PersianCalendar();

            int py = Convert.ToInt32(pdate.GetYear(date));
            int pm = Convert.ToInt32(pdate.GetMonth(date));
            int pd = Convert.ToInt32(pdate.GetDayOfMonth(date));
            int m = Convert.ToInt32(pdate.GetDayOfWeek(date));

            #region
            /*String[] dname = new String[7];
            String[] mon = new String[13];

            dname[0] = "شنبه";
            dname[1] = "یک شنبه";
            dname[2] = "دو شنبه";
            dname[3] = "سه شنبه";
            dname[4] = "چهار شنبه";
            dname[5] = "پنج شنبه";
            dname[6] = "جمعه";

            mon[01] = "فروردین";
            mon[02] = "اردیبهشت";
            mon[03] = "خرداد";
            mon[04] = "تیر";
            mon[05] = "مرداد";
            mon[06] = "شهریور";
            mon[07] = "مهر";
            mon[08] = "آبان";
            mon[09] = "آذر";
            mon[10] = "دی";
            mon[11] = "بهمن";
            mon[12] = "اسفند";
            */
            #endregion

            String spy = "", spm = "", spd = "";
            if (py < 10)
                spy = "0";
            if (pm < 10)
                spm = "0";
            if (pd < 10)
                spd = "0";

            spy += Convert.ToString(py);
            spm += Convert.ToString(pm);
            spd += Convert.ToString(pd);

            String m2s = "";

            if (halat == 0)
                m2s = spy + spm + spd;
            else if (halat == 1)
                m2s = spd;
            else if (halat == 2)
                m2s = spm;
            else if (halat == 3)
                m2s = spy;
            #region
            /*  
            else if (halat == 4)
            {
                if (m == 6)
                    m = 0;
                m2s = dname[m + 1];
            }

            else if (halat == 5)
                m2s = mon[pm];
            else if (halat == 6)
                m2s = m.ToString();
            else if (halat == 7)
                m2s = dname[m] + " " + Convert.ToString(pd) + " " + mon[pm] + " ";
            */
            #endregion
            return m2s;
        }

        //------------------------افزودن نقش جدید----------------------//
        public static bool addNewRole(int unitID, string newRoleName)
        {
            manageCallsEntities2 db = new manageCallsEntities2();
            bool result = false;
            try
            {
                if (db.roles.Any(p => p.roleName.Equals(newRoleName)))
                {
                    return result;
                }
                else
                {
                    role newRole = new role();
                    newRole.roleName = newRoleName;
                    db.roles.Add(newRole);
                
                    db.SaveChanges();
                    
                    result = true;
                }
            }
            catch
            {
                return result;
            }

            return result;
        }
        //-----------------افزودن کاربر جدید-----------------//
        public static bool addNewUser(Array[] userInfo, int _unitID, int _roleID)
        {
            manageCallsEntities2 db = new manageCallsEntities2();
            bool result = false;//نتیجه ی عملیات

            bool gender;
            var userName = userInfo[0].ToString();
            var firstName = userInfo[1].ToString();
            var lastName = userInfo[2].ToString();
            string nationalCode = userInfo[3].ToString();
            var email = userInfo[4].ToString();

            /* تعیین نقش و واحد :/ */

            //تعیین جنسیت 
            try
            {
                if (Convert.ToInt16(userInfo[5]) == 0)//female
                    gender = true;
                else //male
                    gender = false;
            }
            catch
            {
                return result;
            }

            //بررسی عدم تکراری بودن کاربر
            try
            {
                var existUser = db.users.FirstOrDefault(p => p.nationalCode.Equals(nationalCode));
                if (existUser == null)
                {
                    return result;
                }

                    //اضافه کردن کاربر
                else
                {

                    //اضافه کردن به جدول کاربران
                    user newUser = new user();
                    newUser.userName = userName;
                    newUser.name = firstName;
                    newUser.familyName = lastName;
                    newUser.nationalCode = nationalCode;
                    newUser.email = email;
                    newUser.gender = gender;
                    newUser.accepted = true;
                    db.users.Add(newUser);
                    db.SaveChanges();

                    //اضافه کردن به جدول roleinunit
                    roleInUnit newRoleInUnit = new roleInUnit();
                    newRoleInUnit.role_id = _roleID;
                    newRoleInUnit.unit_id = _unitID;
                    newRoleInUnit.user_id = newUser.id;
                    db.roleInUnits.Add(newRoleInUnit);
                    db.SaveChanges();

                    result = true;

                }
            }
            catch
            {
                return result;
            }

            return result;
        }
        //---------------------------------فیلتر کردن گزارش ها---------------------//
        //public static List<view_rawReports> report(string lastName, int? number, string chosenRole, int startDate, int finishDate, int? duration)
        //{
        //    var db = new manageCallsEntities2();
        //    bool l = String.IsNullOrEmpty(lastName);// بر اساس نام خانوادگی
         

        //  var report = from v in db.view_rawReports
        //               where v.callDate.Day >= startDate
        //               && v.callDate.Day <= finishDate
        //                         select v;
          
        //    if (String.IsNullOrEmpty(lastName)==false)// بر اساس نام خانوادگی
        //    {
        //            report = report.Where(r => r.familyName.Contains(lastName));    
        //    }
        //    if (number != null)//بر اساس شماره داخلی
        //        {
        //            report = report.Where(r => r.number.Equals(number));
        //        }
        //    if (String.IsNullOrEmpty(chosenRole)==false)//براساس نقش انتخابی
        //    {
        //            report = report.Where(r => r.roleName.Contains(chosenRole)); 
        //    }
        //    //if(duration != null)//کاربران مشکوک
        //    //{
        //    //    report = report.Where(r => r.callDuration >= duration);
        //    //}
        //    //return report.ToList();
        //}
    }
}