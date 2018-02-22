using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPM.Models.Timesheet
{
    public class TimesheetList
    {
        public DateTime timeSheetId { get; set; }   
        public string taskName { get; set; }
        public string projectName { get; set; }
        public string functionName { get; set; }
        public string approve1 { get; set; }
        public string approve2 { get; set; }
        // public DateTime recordDate { get; set; }


    }
}
