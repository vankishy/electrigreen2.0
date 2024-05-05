using System;
using System.Collections.Generic;
using System.Linq;
using electrigreenAPI.Models;
using electrigreenAPI.Authentication;

namespace electrigreenAPI.Authentication
{
    public class AuthManager
    {
        public List<RegisterModel> _users;

        public AuthManager(List<RegisterModel> users)
        {
            _users = users;
        }

        public bool Authenticate<T>(T model, Func<RegisterModel, T, bool> predicate)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var user = _users.FirstOrDefault(u => predicate(u, model));

            return user != null;
        }
    }
}
