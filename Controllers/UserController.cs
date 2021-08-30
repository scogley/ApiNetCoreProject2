using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiNetCoreProject2.Models;
using System.Collections;

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
        public IEnumerable Get()
        {
            var users = _service.GetAllUsers();
            //return users.ToList()[0].UserId.ToString();
            return users;
        }

        // POST: API/user
        [HttpPost]
        public IActionResult Post([FromBody] UserModel value)
        {
            if (value == null)
            {
                return new BadRequestResult();
            }

            var user = new UserModel();
            user.Email = value.Email;
            user.Password = value.Password;
            user.CreatedDate = DateTime.UtcNow;
            _service.Add(user);

            return CreatedAtAction(nameof(Get), user.UserId, user);
        }

        // TODO
        // PUT {guid} to update a user and return a 200 OK

        // TODO
        // GET {guid} to get a single user
        // GET: api/Contacts/5
        [HttpGet("{guid}", Name = "Get")]
        public IActionResult Get(Guid guid)
        {
            var user = _service.GetAllUsers().FirstOrDefault(t => t.UserId == guid);
            if (user == null)
            {
                return new NotFoundResult();
            }
            return Ok(user);
        }

        // TODO
        // DELETE {guid} to delete a single user and return a 200 or 404 if the user is not found
    }
}
