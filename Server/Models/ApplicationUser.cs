using Microsoft.AspNetCore.Identity;
using Onebrb.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.Server.Models
{
    public class ApplicationUser : IdentityUser<int>, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string About { get; set; }
    }
}
