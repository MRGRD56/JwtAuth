using System;
using System.Collections.Generic;
using Mrgrd56.JwtAuth.Models;

namespace Mrgrd56.JwtAuth.TestApp.Models
{
    public class User
    {
        public Guid Id { get; set; }
        
        public string Login { get; set; }
        
        public string Password { get; private set; }
        
        public List<string> Roles { get; set; }

        public void SetPassword(string password)
        {
            Password = BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }

        public string GenerateAccessToken(JwtOptions jwtOptions)
        {
            return jwtOptions.GenerateToken(Id.ToString(), Login, Roles);
        }

        public User(string login, string password, List<string> roles)
        {
            Id = Guid.NewGuid();
            Login = login;
            SetPassword(password);
            Roles = roles;
        }
        
        public User(Guid id, string login, string password, List<string> roles)
        {
            Id = id;
            Login = login;
            SetPassword(password);
            Roles = roles;
        }
    }
}