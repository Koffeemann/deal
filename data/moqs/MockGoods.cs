using deal.data.Interfaces;
using deal.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace deal.data.moqs
{
    public class MockGoods : IGood
    {
        public IUser _allUsers = new MockUsers();
        public IEnumerable<Good> Goods { 
            get 
            {
                return new List<Good> { 
                    new Good { 
                        available = true, Name = "рофланы",
                        cost = 150, mindesc = "поживиться",
                        maxdesc = "немного сушек",

                    }, new Good {
                        available = true, Name = "рофланы",
                        cost = 150, mindesc = "поживиться",
                        maxdesc = "немного печенья",

                    } , new Good {
                        available = true, Name = "рофланы",
                        cost = 300, mindesc = "поживиться",
                        maxdesc = "и рис с мясом",

                    } };

             }
        }
        public IEnumerable<Good> GetFavGoods { get; set; }

        public Good GetGood(int goodId)
        {
            throw new NotImplementedException();
        }

        //IEnumerable<Good> goods = Goods;
        //List<Good> sellergood = new List<Good>();
        //    for (int i = 0; i <= goods.Count(); i++)
        //        if (goods.ElementAt(i).sellerName.Name == Name)
        //        {

        //            sellergood.Add(goods.ElementAt(i));

        //        }
        //    return sellergood;

        public IEnumerable<Good> GetSellerGood(string Name)
        {

        throw new NotImplementedException();
    }
    }
}
