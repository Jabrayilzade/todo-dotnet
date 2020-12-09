using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWish.Dto
{
    public class UserRegisterDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }
    }
}
