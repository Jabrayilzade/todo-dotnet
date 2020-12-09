using System;
using TodoWish.Models;

namespace TodoWish.Dto
{
    public class CreateProjectDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Due { get; set; }
    }
}