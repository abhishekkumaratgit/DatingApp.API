using DatingApp.API.Data;
using DatingApp.API.DTOs;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.API.Controllers
{
    public class AuthController : APIController
    {
        private readonly IAuthRepository repo;
        private readonly IConfiguration config;

        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            repo = authRepository;
            config = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO user)
        {
            user.username = user.username.ToLower();

            if (await repo.UserExists(user.username))
            {
                return BadRequest("Username already exists");
            }

            var userToCreate = new User { UserName = user.username };
            var createdUser = await repo.Register(userToCreate, user.password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO user)
        {
            var userFromRepo = await repo.Login(user.Username, user.Password);
            if (userFromRepo == null)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName)
            };

            var secret = Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value);
            var key = new SymmetricSecurityKey(secret);

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token) });
        }
    }
}