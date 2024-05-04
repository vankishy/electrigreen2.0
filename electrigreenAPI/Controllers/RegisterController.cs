using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text;
using electrigreenAPI.Models;
using electrigreen;

namespace electrigreenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private const string filePath = "accRecord.json";
        private List<RegisterModel> _users;

        public RegisterController()
        {
            GetRecord();
        }

        private void GetRecord()
        {
            if (System.IO.File.Exists(filePath))
            {
                string json = System.IO.File.ReadAllText(filePath);
                _users = JsonSerializer.Deserialize<List<RegisterModel>>(json);
            }
            else
            {
                _users = new List<RegisterModel>();
            }
        }

        private void SaveRecord()
        {
            string json = JsonSerializer.Serialize(_users);
            System.IO.File.WriteAllText(filePath, json);
        }

        [HttpGet("{nama}")]
        public IActionResult GetUserbyName(string nama)
        {
            if (_users.Count == 0)
            {
                return Ok("Data tidak ada");
            }

            foreach (RegisterModel user in _users)
            {
                if (user.nama == nama)
                {
                    return Ok(user);
                }
            }
            return Ok("Data tidak ada");
        }
    


        [HttpPost("register")]
        public IActionResult Register(RegisterModel model)
        {
            if (_users.Any(u => u.email == model.email))
            {
                return Conflict("hasBeenUsed");
            }

            _users.Add(model);
            SaveRecord();
            return Ok("Akun terdaftar");
        }

        /*[HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            var user = _users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
            if (user == null)
            {
                return NotFound("Email atau password salah.");
            }

            return Ok(user);
        }*/

        /*[HttpGet("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }*/
    }
}