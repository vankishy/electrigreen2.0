using electrigreenAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace electrigreenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly List<User> _users = new List<User>();
        // GET: api/<ValuesController>
        /*[HttpGet]
        public IEnumerable<RegisterModel> Get()
        {
            return _users;
        }*/

        // GET api/<ValuesController>/s
        [HttpGet("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            var user = _users.FirstOrDefault(u => u.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (_users.Any(u => u.email == model.email))
            {
                return Conflict("Username sudah digunakan");
            }

            var user = new User { ID = Guid.NewGuid(), nama = model.nama, email = model.email, password = model.password };
            _users.Add(user);

            return CreatedAtAction(nameof(GetUserById), new { ID = user.ID});
        }

        // PUT api/<ValuesController>
        /*[HttpPut("{id}")]
        public void Put(int id, [FromBody] RegisterModel value)
        {
            _listRegister[id] = value;
        }

        [HttpDelete("{id}")]
        public void Delete(int id) {
            _listRegister.RemoveAt(id);
        }*/
    }
}
