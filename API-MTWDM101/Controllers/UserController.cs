using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_MTWDM101.Helpers;
using API_MTWDM101.Models;
using APIUsers.Library.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API_MTWDM101.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        
        public IEnumerable<APIUsers.Library.Models.User> GetUsers()
        {
#if false
           List<User> users = new List<User>();
            users.Add(new Models.User()
            {
                CreateDate = DateTime.Now,
                ID = 1,
                Name = "Ramón Gerardo",
                Nick = "rgatilanov",
                Password = null,
                accountType = AccountType.Administrator
            });

            users.Add(new Models.User()
            {
                CreateDate = DateTime.Now,
                ID = 2,
                Name = "Juan Perez",
                Nick = "juan.perez",
                Password = null,
                accountType = AccountType.Basic,
            });

#endif
            List<APIUsers.Library.Models.User> listUsers = new List<APIUsers.Library.Models.User>();
            var ConnectionStringLocal = _configuration.GetValue<string>("ServidorLocal");
            using (IUser User = Factorizador.CrearConexionServicio
                (APIUsers.Library.Models.ConnectionType.MSSQL, ConnectionStringLocal))
            {
                listUsers = User.GetUsers();
            }
            return listUsers;
            //return users;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User GetUser(int id)
        {
            Models.User user = null;
            if (id == 1)
                user = new Models.User()
                {
                    CreateDate = DateTime.Now,
                    ID = 1,
                    Nick = "rgatilanov",
                    Password = null,
                    Name = "Ramón Gerardo",
                    accountType = AccountType.Administrator
                };
            else
                user = new Models.User()
                {
                    CreateDate = DateTime.Now,
                    ID = 2,
                    Name = "Juan Perez",
                    Nick = "juan.perez",
                    Password = null,
                    accountType = AccountType.Basic,
                };

            return user;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"> {"id": 3,"nick": "leones2019","password": "123123","createDate": "2019-08-02T12:43:02.9396464-05:00"}
        /// </param>
        /// <returns></returns>
        // POST api/<UserController>
        //[HttpPost]
        //public User PostUser([FromBody] User value)
        //{
        //    /*Lógica a base de datos*/
        //    value.Name = "ACTUALIZADO!!!";
        //    return value;
        //}
        [HttpPost]
        public int InsertUser([FromBody] APIUsers.Library.Models.UserMin value)
        {
            int id = 0;
            var ConnectionStringLocal = _configuration.GetValue<string>("ServidorLocal");
            using (IUser User = Factorizador.CrearConexionServicio(APIUsers.Library.Models.ConnectionType.MSSQL, ConnectionStringLocal))
            {
                id = User.InsertUser(value.Nick, Functions.GetSHA256(value.Password));
            }
            return id;
        }
    }
}
