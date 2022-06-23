using deal.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace deal.data.Interfaces
{
    public interface IGood
    {
        IEnumerable<Good> Goods { get; }
        IEnumerable<Good> GetFavGoods { get; set; }
        Good GetGood(int goodId);
        IEnumerable<Good> GetSellerGood(string Name);
    }
}
