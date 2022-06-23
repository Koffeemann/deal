using deal.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace deal.data.Interfaces
{
    public interface IUser
    {
        IEnumerable<User> Users { get; }
        public User GetUser(int userId);

    }
}
