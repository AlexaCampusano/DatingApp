using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.DTOs;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        public AuthController(IAuthRepository repository)  => _repository = repository;

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegistration)
        {
            userForRegistration.UserName = userForRegistration.UserName.ToLower();

            if(await _repository.UserExists(userForRegistration.UserName))
                return BadRequest("Username already exists.");
            
            var userToCreate = new User {
                UserName = userForRegistration.UserName
            };

            var createdUser = await _repository.Register(userToCreate, userForRegistration.Password);

            return StatusCode(201);
        }
    }
}