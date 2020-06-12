using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Onebrb.Server.Abstractions;
using Onebrb.Server.Interfaces;
using Onebrb.Server.Models;
using Onebrb.Server.Repos;
using Onebrb.Shared.Dtos.Users;

namespace Onebrb.Server.Api
{
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user = await _userRepository.GetAsync(id);

            if (user == null)
            {
                return BadRequest();
            }

            var dto = _mapper.Map<UserDto>(user);

            return Ok(dto);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<UserDto>> GetUserByUserName(string username)
        {
            var user = await _userRepository.GetUserByUserNameAsync(username);

            if (user == null)
            {
                return BadRequest();
            }

            var dto = _mapper.Map<UserDto>(user);

            return Ok(dto);
        }
    }
}
