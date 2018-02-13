using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPM.Models.Timesheet
{
    public class TempTimesheetModel
    {
        public DateTime tempDate;
        public TimeSpan tempSTime { get; set; }
        public TimeSpan tempETime { get; set; }
        public string Strdate { get; set; }
        public string StrdateConcate { get; set; }
        public string StrStime { get; set; }
        public string StrEtime { get; set; }
        public string concateStime { get; set; }
        public string concateEtime { get; set; }
        public string actionName { get; set; }
    }
}
