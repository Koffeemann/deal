using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace deal.data.Models
{
    public class UserHistory
    {
        public List<DeliveryRecord> DeliveryRecords {get; set;}
        public List<Order> Orders { get; set; }
    }
}
