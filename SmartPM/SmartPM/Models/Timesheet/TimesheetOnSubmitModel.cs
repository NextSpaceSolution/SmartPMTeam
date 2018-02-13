using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPM.Models.Timesheet
{
    public class TimesheetOnSubmitModel
    {
        public string TimeSheetId { get; set; }
        public string ActionId { get; set; }
        public string TimeSheetStart { get; set; }
        public string TimeSheetEnd { get; set; }
        public string UserId { get; set; }
        public string FunctionId { get; set; }
        public string TaskId { get; set; }
        public string ProjectNumber { get; set; }
        public string TimeSheetNumber { get; set; }
    }
}
