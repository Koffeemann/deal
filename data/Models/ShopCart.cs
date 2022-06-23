using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace deal.data.Models
{
    public class ShopCart
    {
        public int ID { get; set; }
        public User User { get; set; }
        public Good Good { get; set; }
    }
}
