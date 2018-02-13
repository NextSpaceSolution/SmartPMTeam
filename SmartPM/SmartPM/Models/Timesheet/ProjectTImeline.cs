using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPM.Models.Timesheet
{
    public class ProjectTImeline
    {
        public DateTime functionLogId { get; set; }
        public DateTime functionEnd { get; set; }
        public DateTime? actualEnd { get; set; }
        public string userCommit { get; set; }
        public string taskName { get; set; }
        public string functionName { get; set; }

    }
}
