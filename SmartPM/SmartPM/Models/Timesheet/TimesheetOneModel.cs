using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPM.Models.Timesheet
{
    public class TimesheetOneModel
    {

        public string userId { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string fullName { get; set; }

        public string jobResp { get; set; }

        public string groupId { get; set; }

        public string projectId { get; set; }

        public string projectName { get; set; }

        public string TaskName { get; set; }

        public string taskId { get; set; }

        public string functionName { get; set; }
        public string functionId {get;set;}
        public string actionName { get; set; }
        public string actionId { get; set; }
    }
}
