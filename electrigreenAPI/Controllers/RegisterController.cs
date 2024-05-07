using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text;
using electrigreenAPI.Models;


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
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrWhiteSpace(model.nama))
            {
                throw new ArgumentException("Nama harus diisi terlebih dahulu!!", nameof(model.nama));
            }

            if (string.IsNullOrWhiteSpace(model.email))
            {
                throw new ArgumentException("Email wajib diisi!!", nameof(model.email));
            }

            if (string.IsNullOrWhiteSpace(model.password))
            {
                throw new ArgumentException("Password harus diisi", nameof(model.password));
            }

            if (_users.Any(u => u.email == model.email))
            {
                return Conflict("hasBeenUsed");
            }

            _users.Add(model);
            SaveRecord();

            if (!_users.Contains(model))
            {
                throw new Exception("Registrasi gagal");
            }
            return Ok("Akun terdaftar");
        }

        [HttpPost("Login")]

        public IActionResult Login(LoginModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid login request");
            }

            var user = _users.FirstOrDefault(u => u.email.Equals(model.email, StringComparison.OrdinalIgnoreCase) && u.password == model.password);

            if (user != null)
            {
                return Ok("Selamat datang " + model.email);
            }
            else
            {
                return Conflict("Email atau Password Salah");
            }
        }


        //[HttpPost("login")]    
    }
}