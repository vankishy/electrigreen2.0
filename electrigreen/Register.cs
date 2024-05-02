using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace electrigreen
{
    public class Register<T>
    {
        private List<T> accounts;

        public Register()
        {
            accounts = new List<T>();
        }

        public void AddAccount(T account)
        {
            accounts.Add(account);
        }

        public bool IsPasswordConfirmed(string password, string confirmPassword)
        {
            return password == confirmPassword;
        }

        public void RegisterNewAccount(T account, string password, string confirmPassword)
        {
            if (IsPasswordConfirmed(password, confirmPassword))
            {
                AddAccount(account);
                Console.WriteLine("Akun telah terdaftar");
            }
            else
            {
                Console.WriteLine("Password salah, coba lagi.");
                Console.Write("\nKonfirmasi Password: ");
                string passConfirm = Console.ReadLine();
                RegisterNewAccount(account, password, passConfirm);
            }
        }

        public void DisplayAccounts()
        {
            int i = 1;
            foreach (var account in accounts)
            {
                Console.Write(i + ". ");
                Console.WriteLine(account);
                i++;
            }
        }
    }

    public class Account
    {
        public string Name { get; }
        public string Email { get; }
        public string Password { get; }

        public Account(string name, string email, string password)
        {

            Name = name;
            Email = email;
            Password = password;
        }

        public override string ToString()
        {
            return $"Nama: {Name}, Email: {Email}";
        }
    }
}
