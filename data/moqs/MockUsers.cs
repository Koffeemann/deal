using deal.data.Interfaces;
using deal.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace deal.data.moqs
{
    public class MockUsers : IUser
    {
        public IEnumerable<User> Users => throw new NotImplementedException();

        //public IGood _allGoods = new MockGoods();

        //public IEnumerable<User> Users { get 
        //    { return new List<User> 
        //    { new User 
        //    { balance = 1000, Name = "Arthas", status = "seller"}
        //    };  
        //    } 
        //}

        public User GetUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
} 
