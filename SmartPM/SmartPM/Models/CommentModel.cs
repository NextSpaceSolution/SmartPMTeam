using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPM.Models.Timesheet
{
   public class CommentModel
    {
        public string userId { get; set; }
        public string bNumber { get; set; }
        public string fullName { get; set; }
        public DateTime time { get; set; }
        public string cid { get; set; }
        public string commentDetail { get; set; }

    }
}
