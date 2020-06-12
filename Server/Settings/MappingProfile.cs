using AutoMapper;
using Onebrb.Server.Models;
using Onebrb.Shared.Dtos.Messages;
using Onebrb.Shared.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.Server.Settings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Message, MessageDto>()
                .ReverseMap();

            CreateMap<ApplicationUser, UserDto>()
                .ForMember(x => x.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ReverseMap();
        }
    }
}
