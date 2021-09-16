using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Mrgrd56.JwtAuth.TestApp.Models;
using Mrgrd56.JwtAuth.TestApp.Services;

namespace Mrgrd56.JwtAuth.TestApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtOptionsProvider _jwtOptionsProvider;
        private readonly UsersRepository _usersRepository;

        public AuthController(JwtOptionsProvider jwtOptionsProvider, UsersRepository usersRepository)
        {
            _jwtOptionsProvider = jwtOptionsProvider;
            _usersRepository = usersRepository;
        }

        [HttpPost]
        public IActionResult Login(
            [Required] [FromBody] UserCredentials credentials)
        {
            var user = _usersRepository.Users.FirstOrDefault(u => u.Login == credentials.Login && u.VerifyPassword(credentials.Password));
            if (user == null)
            {
                return Unauthorized("Invalid login or password");
            }

            return GetAuthResult(user);
        }

        [HttpPost]
        public IActionResult Register(
            [Required] [FromBody] UserCredentials credentials)
        {
            if (_usersRepository.Users.Any(u => u.Login == credentials.Login))
            {
                return Conflict("The user with the specified login already exists");
            }
            
            var user = new User(credentials.Login, credentials.Password, new List<string> { "user" });
            _usersRepository.Users.Add(user);

            return GetAuthResult(user);
        }

        private IActionResult GetAuthResult(User user)
        {
            var accessToken = user.GenerateAccessToken(_jwtOptionsProvider.JwtOptions);
            return Ok(new
            {
                user.Id,
                user.Login,
                user.Roles,
                AccessToken = accessToken
            });
        }
    }
}