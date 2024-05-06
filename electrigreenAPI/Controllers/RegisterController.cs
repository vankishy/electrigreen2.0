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

        [HttpGet]
        public IActionResult GetUser()
        {
            if (_users.Count == 0)
            {
                return Ok("Data kosong!!");
            }

            return Ok(_users);
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

        //[HttpPost("login")]    
    }
}