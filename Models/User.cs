using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWish.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string UserRole { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastLog { get; set; }
        public string Token { get; set; }
        public List<Todo> Todos { get; set; }
        public List<Project> Projects { get; set; }
    }
}