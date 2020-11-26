using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Repository;
using Microsoft.Extensions.Configuration;
using AuthService.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AuthService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController:ControllerBase
    {
        private readonly ILogin _loginrepo;
        private IConfiguration _config;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(LoginController));

        // GET: api/Login
        public LoginController(IConfiguration config, ILogin login)
        {
            _config = config;
            _loginrepo = login;
        }
        [Route("api/Login/{name}")]
        [HttpGet]
        public User GetUser(string username)
        {
            var u = _loginrepo.GetUser(username);
            return u;
        }


        //[HttpGet]
        //public IActionResult Get()
        //{
        //  return Content("Ok");
        //}

        // POST: api/Login
        [HttpPost]
        public IActionResult Login([FromBody] User Login)
        {
            try
            {
                IActionResult response = Unauthorized();
                _log4net.Info("Login attempt made by" + Login.Username);
                User u = GetUser(Login.Username);
                if (u != null) {
                    if (Login.Username == u.Username && Login.Password == u.Password)
                    {
                        var tokenString = GenerateJWTToken(Login);
                        _log4net.Info("Token generated for" + Login.Username);
                        response = Ok(new { token = tokenString }); ;

                    } 
                }
                return response;
            }
            catch
            {
                _log4net.Error("Login attempt failed by" + Login.Username);
                return BadRequest();
            }
        }
        private string GenerateJWTToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
               _config["Jwt:Issuer"],
               null,
               expires: DateTime.Now.AddMinutes(30),
               signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token).ToString();



        }
    }
}
