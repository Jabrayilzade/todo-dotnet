using System;

namespace TodoWish.Dto
{
    public class CreateTaskDto
    {
        public string Content { get; set; }
        public DateTime Due { get; set; }
    }
}