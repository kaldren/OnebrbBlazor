using System;
using System.Collections.Generic;
using System.Text;

namespace Onebrb.Shared.Dtos.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Profession { get; set; }
        public string About { get; set; }
    }
}
