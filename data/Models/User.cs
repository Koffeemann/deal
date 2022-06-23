using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace deal.data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsOrganization { get; set; }
        public ushort? balance { get; set; }
        public long phone { get; set; }
        public string password { get; set; }
    }
}
