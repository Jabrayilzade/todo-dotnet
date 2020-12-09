namespace TodoWish.Dto
{
    public class GetStatisticsDto
    {
        public int UserId { get; set; }
        public int Tasks { get; set; }
        public int Completed { get; set; }
        public int Overdue { get; set; }
        public int Performance { get; set; }
        public int Ongoing { get; set; }
    }
}