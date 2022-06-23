using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using deal.data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace deal.Controllers
{
    public class DeliverymanPageController : Controller
    {
        Context db;
        public DeliverymanPageController(Context context)
        {
            db = context;
        }
        public IActionResult SignIn()
        {
            return View();
        }
        public IActionResult History()
        {
            if(GetUser())
            {
                List<DeliveryRecord> records = db.DeliveryRecords.Include(p => p.Order.Good).Where(p => p.deliveryman.Id == int.Parse(HttpContext.Request.Cookies["wty"]) && p.status != "принят").ToList();
                return View(records);
            }
            else
            {
                return RedirectToAction("SignIn");
            }

        }
        public IActionResult WorkPage()
        {
            if (GetUser())
            {
                List<DeliveryRecord> records = db.DeliveryRecords.Include(p => p.Order.Good).Where(p => p.deliveryman == null).ToList();
                return View(records);
            }
            else
            {
               return RedirectToAction("SignIn");
            }

        }
        public IActionResult Accepted()
        {
            if (GetUser())
            {
                List<DeliveryRecord> records = db.DeliveryRecords.Include(p => p.Order.Good).Include(p => p.Order.User).Where(p => p.deliveryman.Id == int.Parse(HttpContext.Request.Cookies["wty"]) && p.status != "доставлен").ToList();
                return View(records);
            }
            else
            {

                return RedirectToAction("SignIn");
            }

        }
        [HttpPost]
        public IActionResult SignIn(Deliveryman worker)
        {
            var acc = db.Deliverymens.Where(p => p.Name == worker.Name && p.SecondName == worker.SecondName && p.password == worker.password).ToList();
            Deliveryman worker1 = acc.FirstOrDefault();
            if (worker1 != null)
            {
                HttpContext.Response.Cookies.Append("wty", worker1.Id.ToString());
                HttpContext.Response.Cookies.Append("wname", worker1.Name);
                HttpContext.Response.Cookies.Append("wsname", worker1.SecondName);
                return RedirectToAction("WorkPage");
            }
            else
            {
                ViewBag.Message = string.Format("Не удалось найти пользователя с таким телефоном и паролем. Попробуйте снова.");
                return View("SignIn");
            }
        }
        [HttpPost]
        public IActionResult Change(int Id,string option)
        {
            DeliveryRecord deliveryRecord = db.DeliveryRecords.SingleOrDefault(p => p.Id == Id);
            deliveryRecord.status = option;
            db.SaveChanges();
            return RedirectToAction("Accepted");
        }
        [HttpPost]
        public async Task<IActionResult> Accept(int Id)
        {
            DeliveryRecord record = await db.DeliveryRecords.SingleOrDefaultAsync(p => p.Id == Id);
            record.deliveryman = await db.Deliverymens.SingleOrDefaultAsync(p => p.Id == int.Parse(HttpContext.Request.Cookies["wty"]));
            record.status = "принят";
            db.SaveChanges();
            return RedirectToAction("WorkPage");
        }
        protected bool GetUser()
        {
            if (HttpContext.Request.Cookies["wty"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
