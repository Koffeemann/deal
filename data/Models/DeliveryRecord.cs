using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace deal.data.Models
{
    public class DeliveryRecord
    {
        public int Id { get; set; }
        public Order Order { get; set;}
        public Deliveryman deliveryman { get; set; }
        public DateTime deliveryDate { get; set; }
        public string Adress { get; set; }
        public string status { get; set; }
    }
}
