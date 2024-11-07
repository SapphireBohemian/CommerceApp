namespace CommerceApp.Models
{
    public class HealthMetricsViewModel
    {
        public Dictionary<string, int> CheckupsByMonth { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> OverdueCheckupsByAgeGroup { get; set; } = new Dictionary<string, int>();
    }

}
