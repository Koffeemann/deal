using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using deal.data.Models;
using Npgsql.EntityFrameworkCore;
using System.Web;
using Microsoft.AspNetCore.Authorization;

namespace deal.Controllers
{
    public class AdminPageController : Controller
    {
        private Context db;
        public AdminPageController(Context context)
        {
            db = context;

        }
        public IActionResult SignIn()
        {

            return View();
        }
       
        public IActionResult WorkPage()
        {
            if(GetUser() && HttpContext.Request.Cookies["wrole"] == "admin")
            {
                return View(db);
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }
        public IActionResult ModerPage()
        {
            if (GetUser())
            {
                List<Good> statements = db.Goods.Where(p => p.available == false).ToList();
                return View(statements);
            }
            else
            {

                return RedirectToAction("SignIn");
            }
        }
        [HttpPost]
        public IActionResult SignIn(Worker worker)
        {
               var acc = db.Workers.Where(p => p.Name == worker.Name && p.SecondName == worker.SecondName && p.password == worker.password).ToList();
                Worker worker1 = acc.FirstOrDefault();
            if (worker1 != null)
            {
                HttpContext.Response.Cookies.Append("wname", worker1.Name);
                HttpContext.Response.Cookies.Append("wsname", worker1.SecondName);
                HttpContext.Response.Cookies.Append("wrole", worker1.role);
                switch (worker1.role)
                 {
                    case "admin":
                        {
                            return RedirectToAction("WorkPage"); 
                        }
                    case "moderator":
                        {
                            return RedirectToAction("ModerPage");
                        }
                    default:
                        {

                            return RedirectToAction("SignIn");
                        }
                }


            }
            else { ViewBag.Message = string.Format("Не удалось найти пользователя с такими данными. Попробуйте снова.");
                return View("SignIn"); }
           
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id, string view) {

            db.Goods.Remove(db.Goods.SingleOrDefault(p => p.Id == Id));
            await db.SaveChangesAsync();
            return RedirectToAction(view);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            db.Users.Remove(db.Users.SingleOrDefault(p => p.Id == Id));
            await db.SaveChangesAsync();
            return RedirectToAction("WorkPage");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteWorker(int Id)
        {
            db.Workers.Remove(db.Workers.SingleOrDefault(p => p.Id == Id));
            await db.SaveChangesAsync();
            return RedirectToAction("WorkPage");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteDeliveryman(int Id)
        {
            db.Deliverymens.Remove(db.Deliverymens.SingleOrDefault(p => p.Id == Id));
            await db.SaveChangesAsync();
            return RedirectToAction("WorkPage");
        }
        [HttpPost]
        public async Task<IActionResult> Release(int id, string Name, string mindesc, string maxdesc, string cost, string Img, string view)
        {
            var good = db.Goods.SingleOrDefault(p => p.Id == id);
            good.available = true;
            good.Name = Name;
            good.mindesc = mindesc;
            good.Img = Img;
            good.maxdesc = maxdesc;
            good.cost = ushort.Parse(cost);
            await db.SaveChangesAsync();
            return RedirectToAction(view);
        }
        [HttpPost]
        public IActionResult CreateWorker(string Name, string SecName, string password, long phone)
        {
            if (PasswordCheck(password))
            {
                Worker worker = new Worker();
                worker.Name = Name;
                worker.SecondName = SecName;
                worker.password = password;
                worker.phone = phone;
                worker.role = "moderator";
                db.Workers.Add(worker);
                db.SaveChanges();
            }
            else
            {
                ViewBag.message1 = "Пароль должен содержать 3 символа алфавита и 3 цифры минимум.";
                return View("WorkPage", db);
            }

            return RedirectToAction("WorkPage");
        }
        [HttpPost]
        public IActionResult CreateDeliveryman(string Name, string SecName, string password, long phone)
        {
            if (PasswordCheck(password))
            {
                Deliveryman worker = new Deliveryman();
                worker.Name = Name;
                worker.SecondName = SecName;
                worker.password = password;
                worker.phone = phone;
                db.Deliverymens.Add(worker);
                db.SaveChanges();
                return RedirectToAction("WorkPage");
            }
            else
            {
                ViewBag.message2 = "Пароль должен содержать 3 символа алфавита и 3 цифры минимум.";
                return View("WorkPage", db);
            }
        }
        [HttpPost]
        public IActionResult OrgStatus(int Id)
        {
            db.Users.Single(p => p.Id == Id).IsOrganization = true;
            db.SaveChanges();
            return RedirectToAction("WorkPage");
        }
        [HttpPost]
        public IActionResult ChangeWorker(int Id, string Name, string SecName, string password, long phone)
        {
            Worker worker = db.Workers.SingleOrDefault(p=>p.Id == Id);
            worker.Name = Name;
            worker.SecondName = SecName;
            worker.password = password;
            worker.phone = phone;
            db.SaveChanges();
            return RedirectToAction("WorkPage");
        }
        [HttpPost]
        public IActionResult ChangeDeliveryman(int Id, string Name, string SecName, string password, long phone)
        {
            Deliveryman worker = db.Deliverymens.SingleOrDefault(p => p.Id == Id);
            worker.Name = Name;
            worker.SecondName = SecName;
            worker.password = password;
            worker.phone = phone;
            db.SaveChanges();
            return RedirectToAction("WorkPage");
        }
        protected bool GetUser()
        {
            if (HttpContext.Request.Cookies["wsname"] != null && HttpContext.Request.Cookies["wname"] != null && HttpContext.Request.Cookies["wrole"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected bool PasswordCheck(string password)
        {
            int l = 0;
            int n = 0; if (password != null) {
                for (int i = 0; i < password.Length; i++)
                {
                    if (Char.IsNumber(password[i]))
                    {
                        n++;
                    }
                    else
                    {
                        if (Char.IsLetter(password[i]))
                        {
                            l++;
                        }
                    }
                }
            }
            if (l >= 3 && n >= 3) { return true; }
            else { return false; }

        }
    }
}
