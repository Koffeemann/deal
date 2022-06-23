using deal.data.Interfaces;
using deal.data.moqs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using deal.data.Models;
using Microsoft.EntityFrameworkCore;
namespace deal.Controllers
{
    public class HomeController : Controller
    {
        private Context db;

        public HomeController(Context context)
        {

            db = context;

        }

        public async Task<IActionResult> Index()
        {
            ViewBag.List = db.Categories.ToList();
            if (
            HttpContext.Request.Cookies["user"] != null)
            {
                ViewBag.I = HttpContext.Request.Cookies["user"];
                ViewBag.U = HttpContext.Request.Cookies["name"];
                ViewBag.B = HttpContext.Request.Cookies["balance"];
            }
            return View(await db.Goods.Where(p => p.available == true).ToListAsync());
        }
        public IActionResult New()
        {
            New newobj = new New();
            newobj.categories = db.Categories.ToList();
            return View(newobj);
        }
        public IActionResult Advertisements()
        {
            if (GetUser())
            {
                List<Good> goods = db.Goods.Include(p => p.category).Where(p => p.sellerName.Id == int.Parse(HttpContext.Request.Cookies["user"])).ToList();
                return View(goods);
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }
        public IActionResult OrdersView()
        {
            if (GetUser())
            {
                List<Order> orders = db.Orders.Include(p => p.User).Include(p => p.Good).Where(p => p.Good.sellerName.Id == int.Parse(HttpContext.Request.Cookies["user"])).ToList();
                return View(orders);
            }
            else
            {
                return RedirectToAction("SignIn");
            }
           
        }
        public IActionResult Purchases()
        {
            if (GetUser())
            {
                UserHistory history = new UserHistory();
                history.Orders = db.Orders.Include(p => p.Good).Where(p => p.User.Id == int.Parse(HttpContext.Request.Cookies["user"])).ToList();
                history.DeliveryRecords = db.DeliveryRecords.Include(p => p.Order.Good).Include(p => p.deliveryman).Where(p => p.Order.User.Id == int.Parse(HttpContext.Request.Cookies["user"])).ToList();
                for (int i = 0; i < history.DeliveryRecords.Count; i++)
                {
                    history.Orders.Remove(history.DeliveryRecords[i].Order);
                }
                return View(history);
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }
        [HttpPost]
        public IActionResult Buy(int Id, int count)
        {
            if (GetUser())
            {
                User user = db.Users.SingleOrDefault(p => p.Id == int.Parse(HttpContext.Request.Cookies["user"]));
                if (user.balance > db.Goods.SingleOrDefault(p => p.Id == Id).cost)
                {
                    Order neworder = new Order();
                    neworder.date = DateTime.Now;
                    neworder.Good = db.Goods.SingleOrDefault(p => p.Id == Id);
                    neworder.User = db.Users.SingleOrDefault(p => p.Id == int.Parse(HttpContext.Request.Cookies["user"]));
                    neworder.count = count;
                    db.Orders.Add(neworder);
                    user.balance -= db.Goods.SingleOrDefault(p => p.Id == Id).cost;
                    neworder.Good.count -= count;
                    HttpContext.Response.Cookies.Append("balance", user.balance.ToString());
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "У вас не хватает средств на покупку";
                    return View("GoodInf", db.Goods.Include(p => p.sellerName).SingleOrDefault(p => p.Id == Id));
                }
            }
            else
            {
                return RedirectToAction("SignIn");
            }
           
            
        }
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            db.Goods.SingleOrDefault(p => p.Id == Id).available = false;
            db.SaveChanges();
            return RedirectToAction("Advertisements");
        }
        [HttpPost]
        public IActionResult New(Good good, int category)
        {
            good.category = db.Categories.Single(p => p.Id == category);
            good.sellerName = db.Users.SingleOrDefault(p => p.Id == int.Parse(HttpContext.Request.Cookies["user"]));
            db.Goods.Add(good);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Adress(int Id)
        {
            if (GetUser())
            {
                User user = db.Users.SingleOrDefault(p => p.Id == int.Parse(HttpContext.Request.Cookies["user"]));
                if (user.balance > db.Goods.SingleOrDefault(p => p.Id == Id).cost)
                {
                    Order neworder = new Order();
                    neworder.date = DateTime.Now;
                    neworder.Good = db.Goods.SingleOrDefault(p => p.Id == Id);
                    neworder.User = db.Users.SingleOrDefault(p => p.Id == int.Parse(HttpContext.Request.Cookies["user"]));
                    db.Orders.Add(neworder);
                    user.balance -= db.Goods.SingleOrDefault(p => p.Id == Id).cost;
                    db.SaveChanges();
                    return View(neworder);
                }
                else
                {
                    ViewBag.Message = "У вас не хватает средств на покупку";
                    return View("GoodInf", db.Goods.Include(p => p.sellerName).SingleOrDefault(p => p.Id == Id));
                }
            }
            else
            {
                return RedirectToAction("SignIn");
            }
           

        }
        public IActionResult ShopCart()
        {
            if (GetUser()) {
                int id = int.Parse(HttpContext.Request.Cookies["user"]);
                User user = db.Users.Single(p => p.Id == id);
                List<ShopCart> list = db.ShopCarts.Include(p => p.Good).Where(p => p.User == user).ToList();
                return View(list);
            }
            else
            {
                return RedirectToAction("SignIn");

            }
        }
        public async Task<IActionResult> GoodInf(int Id)
        {
           
            return View(await db.Goods.Include(p => p.sellerName).SingleOrDefaultAsync(p => p.Id == Id));
        }
      
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DeliveryOrder(string[] adress, int Id)
        {
            Order order = db.Orders.SingleOrDefault(p => p.Id == Id);
            string adressstring =""; 
            for(int i=0;i < adress.Length; i++)
            {
                adressstring += adress[i];
            }
            DeliveryRecord record = new DeliveryRecord();
            record.Adress = adressstring;
            record.deliveryDate = order.date.AddDays(15);
            record.Order = order;
            db.DeliveryRecords.Add(record);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult TopUp(int count)
        {
             var user = db.Users.SingleOrDefault(p => p.Id == int.Parse(HttpContext.Request.Cookies["user"]));
            string countstring = Convert.ToString(count);
                user.balance += ushort.Parse(countstring);
            db.SaveChanges();
            HttpContext.Response.Cookies.Delete("balance");
            HttpContext.Response.Cookies.Append("balance", user.balance.ToString());
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Filters(string min, string max, int[] categoryarray)
        {
            int minimum = int.Parse(min);
            int maximum = int.Parse(max);
            List<int> category = categoryarray.ToList();
            ViewBag.List = db.Categories.ToList();
            List<Good> filtredgoods = new List<Good>();
            if(category.Count > 0) {
                for (int i = 0; i < category.Count; i++)
                {
                        int j = 0;
                        filtredgoods.AddRange(db.Goods.Where(p => p.category.Id == category[i] && p.cost < maximum && p.cost > minimum));
                    
                }
            }
            else {  filtredgoods = db.Goods.Where(p => p.cost > minimum && p.cost < maximum).ToList(); }
            ViewBag.List = db.Categories.ToList();
            return View("Index", filtredgoods.Where(p => p.available == true).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchstring)
        {
            List<Good> goods = db.Goods.ToList();
            List<Good> searchgoods = new List<Good>();
             for (int i = 0; i < goods.Count; i++)
            {
                if (goods[i].mindesc.Contains(searchstring) ||
                    goods[i].maxdesc.Contains(searchstring) ||
                    goods[i].Name.Contains(searchstring))
                {
                    searchgoods.Add(goods[i]);
                }
            }

            ViewBag.List = db.Categories.ToList();
            return View("Index", searchgoods.Where(p => p.available == true).ToList());
            
           
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if(user.Name != null && user.phone > 1000000000 && user.password.Length >= 7 && PasswordCheck(user.password))
            {
                user.balance = 0;
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Не все поля заполнены. Пароль должен содержать не меньше 7 символов." +
                    "Пароль должен содержать 3 символа алфавита и 3 цифры.";
                return View();
            }

        }

        [HttpPost]
        public IActionResult Shopcart2(int id)
        {
            if (GetUser())
            {
                ShopCart elem = new ShopCart();
                string idu = HttpContext.Request.Cookies["user"];
                elem.Good = db.Goods.Single(p => p.Id == id);
                elem.User = db.Users.Single(p => p.Id == Convert.ToInt32(idu));
                db.ShopCarts.Add(elem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("SignIn");
            }
           

        }

        [HttpPost]
        public async Task<IActionResult> ShopCartDelete(int id)
        {
            ShopCart card = db.ShopCarts.Include(p => p.Good).Single(p => p.ID == id);
            db.ShopCarts.Remove(card);
            await db.SaveChangesAsync();
            return RedirectToAction("ShopCart");
        }

        [HttpPost]
        public async Task<IActionResult> SignIn2(User user)
        {
            User usersign = await db.Users.SingleOrDefaultAsync(p => p.phone == user.phone && p.password == user.password);
            
            if (usersign != null)
            {
                HttpContext.Response.Cookies.Append("user", usersign.Id.ToString());
                HttpContext.Response.Cookies.Append("balance", usersign.balance.ToString());
                HttpContext.Response.Cookies.Append("name", usersign.Name); 
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = string.Format("Не удалось найти пользователя с таким телефоном и паролем. Попробуйте снова.");
                return View("SignIn");
            }
        }
        protected bool GetUser()
        {
            if (HttpContext.Request.Cookies["name"] != null && HttpContext.Request.Cookies["balance"] != null && HttpContext.Request.Cookies["user"] != null)
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
            int n =0;
            for(int i =0; i < password.Length; i++)
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
            if(l>=3 && n >= 3) { return true; }
            else { return false; }
            
        }
    }
}
