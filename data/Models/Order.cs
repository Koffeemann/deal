using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace deal.data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Good Good { get; set; }
        public DateTime date { get; set; }
        public int count { get; set; }

    }
}
