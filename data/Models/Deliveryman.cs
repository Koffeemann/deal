using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace deal.data.Models
{
    public class Deliveryman
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string SecondName { get; set; }
        public long phone { get; set; }
        public string password { get; set; }
    }
}
