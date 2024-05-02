using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace electrigreenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private static List<Register> listRegister = new List<Register>
        {
            new Register()
            {
                
            }
        };
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Register> Get()
        {
            return listRegister;
        }

        // GET api/<ValuesController>/s
        [HttpGet("{id}")]
        public Register Get(int id)
        {
            return listRegister[id];
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] Register value)
        {
            listRegister.Add(value);
        }

        // PUT api/<ValuesController>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Register value)
        {
            listRegister[id] = value;
        }

        [HttpDelete("{id}")]
        public void Delete(int id) {
            listRegister.RemoveAt(id);
        }
    }
}
