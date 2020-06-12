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

namespace Onebrb.Server.Api
{
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    public class UsersController : BaseController<ApplicationUser, IUserRepository>
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
