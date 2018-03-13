using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPM.Models
{
   public class ProjectInfo
    {
        public string projectNumber { get; set; }
        public string projectId { get; set; }
        public string projectName { get; set; }
        public string projectManagerfName { get; set; }
        public string projectManagerlName { get; set; }
        public DateTime? projectStart { get; set; }
        public DateTime? projectEnd { get; set; }
        public long projectCost { get; set; }
        public string projectCreateBy { get; set; }
        public DateTime? projectCreateDate { get; set; }
        public object projectEditBy { get; set; }
        public object projectEditDate { get; set; }
        public string customerName { get; set; }
        public string customerTel { get; set; }
        public DateTime? actualStart { get; set; }
        public DateTime? actualEnd { get; set; }
        public string note { get; set; }
        public string variant { get; set; }
        public string projectStatus { get; set; }
    }
}
