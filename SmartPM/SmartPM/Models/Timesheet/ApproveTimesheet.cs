using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPM.Models.Timesheet
{
    public class ApproveTimesheet
    {
        public DateTime submitDate { get; set; }
        public string projectId { get; set; }
        public string taskId { get; set; }
        public string functionId { get; set; }
        public string actionId { get; set; }
        public string userId { get; set; }
        public string timeStart { get; set; }
        public string timeEnd { get; set; }
    }

}

