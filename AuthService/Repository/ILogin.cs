using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Models;

namespace AuthService.Repository
{
    public interface ILogin
    {
        public User GetUser(string uname);
    }
}
