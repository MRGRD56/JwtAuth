using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Mrgrd56.JwtAuth.Extensions;
using Mrgrd56.JwtAuth.TestApp.Models;

namespace Mrgrd56.JwtAuth.TestApp.Services
{
    public class UsersRepository
    {
        public readonly List<User> Users = new()
        {
            new User(Guid.Parse("b76f4adb-c449-4565-9f2d-a7390f9da4c2"), "admin", "@dmin", new List<string> { "admin", "user" }),
            new User(Guid.Parse("f1d96ec8-f224-4de3-bccf-ea861ff0772b"), "user123", "123123", new List<string> { "user" })
        };

        public User GetCurrent(ClaimsPrincipal claimsPrincipal)
        {
            var idClaim = claimsPrincipal.GetIdClaim();
            if (idClaim == null) return null;
            
            var userId = Guid.Parse(idClaim.Value);
            var user = Users.SingleOrDefault(u => u.Id == userId);
            return user;
        }
    }
}