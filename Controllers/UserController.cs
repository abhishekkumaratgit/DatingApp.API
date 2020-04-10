using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingApp.API.Controllers
{
    public class UserController : APIController
    {
        private readonly IDatingRepository repo;
        private readonly IMapper mapper;

        public UserController(IDatingRepository repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<UserForListDTO>> GetUsers()
        {
            var users = await repo.GetUsers();
            var usersDTO = mapper.Map<IEnumerable<UserForListDTO>>(users);
            return usersDTO;
        }

        [HttpGet("{id:int}")]
        public async Task<UserForDetailDTO> GetUserById(int id)
        {
            var user = await repo.GetUser(id);
            var userDTO = mapper.Map<UserForDetailDTO>(user);
            return userDTO;
        }
    }
}