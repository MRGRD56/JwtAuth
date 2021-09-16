using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Mrgrd56.JwtAuth.TestApp.Services;

namespace Mrgrd56.JwtAuth.TestApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly UsersRepository _usersRepository;

        public ProfileController(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            var currentUser = _usersRepository.GetCurrent(User);

            return Ok(new
            {
                currentUser.Id,
                currentUser.Login,
                currentUser.Roles
            });
        }
    }
}