using Microsoft.AspNetCore.Mvc;
using ProjectTwo.Controllers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static int currentId = 101;
        private static List<User> users = new List<User>();

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return users;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            var user = users.FirstOrDefault(t => t.UserId == id);
            return user;
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] User value)
        {
            if(value == null )
            {
                return new BadRequestResult();
            }
            if(value.UserEmail == null || value.UserPassword == null)
            {
                return new BadRequestResult();
            }

            value.UserId = currentId;
            value.CreatedDate = DateTime.UtcNow;

            users.Add(value);
            currentId++;

            var result = value;

            return CreatedAtAction(nameof(Get), new { id = value.UserId }, result);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User value)
        {
            if (value.UserEmail == null || value.UserPassword == null)
            {
                return new BadRequestResult();
            }

            var user = users.FirstOrDefault(t => t.UserId == id);

            if(user == null)
            {
                return NotFound();
            }

            user.UserEmail = value.UserEmail;
            user.UserPassword = value.UserPassword;

            return Ok();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usersDeleted = users.RemoveAll(t => t.UserId == id);

            if(usersDeleted == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }
    }
}
