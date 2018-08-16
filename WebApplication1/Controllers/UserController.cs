using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        // GET: User
        public ActionResult Signup(int id = 0)
        {

            Signup signupModel = new Signup();
            return View(signupModel);
        }
        [HttpPost]
        public ActionResult Signup(Signup signupModel)
        {
            using (manageCallsEntities2 Db = new manageCallsEntities2())
            {
                if (Db.users.Any(x => x.userName == signupModel.username))
                {
                    ViewBag.DuplicateMessage = "username already exist";
                    return View("Signup", signupModel);
                }
                user u = new user
                { userName = signupModel.username,
                    password = signupModel.password,
                    familyName = signupModel.familyName,
                    nationalCode = signupModel.nationalityCode,
                    name = signupModel.name,
                    email = signupModel.email,
                    gender = signupModel.gender,
                    accepted = true,
                    createDate = DateTime.Now
                };
                Db.users.Add(u);
                Db.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "successful";
            return View("Signup", new Signup());
        }
        [HttpGet]
        public ActionResult Login()
        {
            Login userLogin = new Login();
            return View(userLogin);
        }
        [HttpPost]
        public ActionResult Login(Login userLogin)
        {
            List<user> query = null;
            using (manageCallsEntities2 db = new manageCallsEntities2())
            {
                query = (from y in db.users
                         where (y.userName == userLogin.username && y.password == userLogin.password)
                         select y).ToList();
            }

            if (query.Count != 0)
            {
                return RedirectToAction("index", "AdminViewCalls");
            }
            else
            {

                ViewBag.SuccessMessage = "تلاش مجدد ";
                return View("Login", new Login());
            }
        }
    }
}
