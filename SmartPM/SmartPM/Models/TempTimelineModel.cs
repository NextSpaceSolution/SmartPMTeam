using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPM.Models
{
    public class TempTimelineModel
    {

        public string TimelineId { get; set; }
        public string ProjectNumber { get; set; }
        public DateTime TimelineDate { get; set; }
        public string Header { get; set; }
        public string Note { get; set; }
       // public bool IsLast { get; set; }
    }
}
