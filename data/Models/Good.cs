using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace deal.data.Models
{
    public class Good
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public string mindesc { get; set; }
        public string maxdesc { get; set; }
        public ushort cost { get; set; }
        public bool available { get; set; }
        public int count { get; set; }
        public User sellerName { get; set; }
        public Category category { get; set; }

    }
}
