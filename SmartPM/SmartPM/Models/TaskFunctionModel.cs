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
        public string functionstart { get; set; }
        public string functionend { get; set; }
        public string actualstart { get; set; }
        public string actualend { get; set; }
        public string variant { get; set; }
        public string projectNumber { get; set; }
        public string team { get; set; }

        public string picture { get; set; }

        public string backclr { get; set; }
    }

    public class TaskFunctions
    {
        List<TaskFunctionModel> taskFunctions { get; set; }
    }

}
