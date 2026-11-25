
namespace LogAnalyticsGUI
{
    public class LogFilter
    {
        public DateTime? StartTime { get; set; } = null;
        public DateTime? EndTime { get; set; } = null;
        public Byte WarningLevel { get; set; } = 0;
        public Byte ThreadId { get; set; } = 0;

        //public string SearchText { get; set; } = string.Empty; 
    }
}
