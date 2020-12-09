using System;

namespace TodoWish.Dto
{
    public class GetProjectDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Due { get; set; }
        public string Content { get; set; }
    }
}