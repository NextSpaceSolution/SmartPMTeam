using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPM.Models
{
  public  class TaskModel2
    {
        public string taskId { get; set; }
        public string projectNumber { get; set; }
        public string taskName { get; set; }
        public string taskStart { get; set; }
        public string taskEnd { get; set; }
        public string actualStart { get; set; }
        public string actualEnd { get; set; }

        public string variant { get; set; }
    }
}
