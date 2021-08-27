using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiNetCoreProject2.Models;

namespace ApiNetCoreProject2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region dependency injection
        private readonly IUserManagerService _service;
        public UserController(IUserManagerService service)
        {
            _service = service;
        }
        #endregion

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            var users = _service.GetAllUsers();
            return users.ToList()[0].UserId.ToString();
        }
    }
}
