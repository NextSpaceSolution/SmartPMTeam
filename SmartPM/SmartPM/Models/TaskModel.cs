using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPM.Models
{
    public class TaskModel
    {
            public string taskId { get; set; }

            public string projectnumber { get; set; }
            
            public string taskname { get; set; }
        
            public string taskstart { get; set; }

            public string taskend { get; set; }

            public string actualstart { get; set; }

            public string actualend { get; set; }

            public string variant { get; set; }
        public string team { get; set; }

        public string picture { get; set; }

        public string backclr { get; set; }

    }
    public partial class TaskModels
    {
        public List<TaskModel> taskModels { get; set; }
    }

}
