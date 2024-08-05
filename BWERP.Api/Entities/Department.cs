namespace BWERP.Api.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<DailyReport>? DailyReports { get; set; }
		public List<Comment>? Comments { get; set; }
	}
}
