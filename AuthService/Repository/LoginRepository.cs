using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Models;

namespace AuthService.Repository
{
    public class LoginRepository:ILogin
    {
        private static readonly List<User> userList = new List<User>()
        {
            new User{ Username="moana",Password="pmoana"},
            new User{ Username="elsa",Password="pelsa"},
            new User{ Username="anna",Password="panna"},
            new User{ Username="ariel",Password="pariel"},
            new User{ Username="aurora",Password="paurora"}

        };

        public User GetUser(string uname)
        {
            User u = userList.Where(s => s.Username == uname).FirstOrDefault();
            return u;
        }
    }
}
