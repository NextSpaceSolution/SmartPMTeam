using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPM.Models
{
    public class TaskFunctionModel
    {

        public string functionId { get; set; }
        public string taskId { get; set; }
        public string functionName { get; set; }
        public DateTime? functionStart { get; set; }
        public DateTime? functionEnd { get; set; }
        public DateTime? actualStart { get; set; }
        public DateTime? actualEnd { get; set; }
        public int? variant { get; set; }
        public string projectNumber { get; set; }


    }



}
