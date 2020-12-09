using System;

namespace TodoWish.Dto
{
    public class GetTaskDto
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime Due { get; set; }
        public string Status { get; set; }
    }
}