using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPM.Models
{
    public class TaskModel
    {
        public string taskId { get; set; }
        public string projectNumber { get; set; }
        public string taskName { get; set; }
        public DateTime? taskStart { get; set; }
        public DateTime? taskEnd { get; set; }
        public DateTime? actualStart { get; set; }
        public DateTime? actualEnd { get; set; }
        public int? variant { get; set; }

    }
   
}
