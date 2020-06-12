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
using Onebrb.Shared.Dtos;

namespace Onebrb.Server.Api
{
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }
    }
}
