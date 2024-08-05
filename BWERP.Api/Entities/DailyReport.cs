namespace BWERP.Api.Entities
{
    public class DailyReport
    {
        public int Id { get; set; }
        public string TodayTask { get; set; }
        public string TomorrowTask { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set;}

        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
