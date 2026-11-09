//using System;
//using System.Security.Permissions;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace LogAnalyticsGUI
{
    public class LogFilterCriteria
    {
        public DateTime StartTime {  get; set; } = DateTime.MinValue;
        public DateTime EndTime { get; set; } = DateTime.MaxValue;
        public Byte WarningLevel { get; set; } = 0;
        public Byte ThreadId { get; set; } = 0;
        //public string SearchText { get; set; } = string.Empty; 
    }
}
