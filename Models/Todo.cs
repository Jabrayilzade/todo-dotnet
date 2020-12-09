using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWish.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime Due { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; } 
        public User User { get; set; }

    }
}
