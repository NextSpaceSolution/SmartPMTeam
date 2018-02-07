using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPM.Models
{
    public class AProjectList
    {
      
            public string projectNumber { get; set; }
            public string projectId { get; set; }
            public string projectName { get; set; }
            public string projectManager { get; set; }
        public string projectStart { get; set; }
        public string projectEnd{ get; set; }
       // public DateTime projectStart { get; set; }
          //  public DateTime projectEnd { get; set; }
            public string projectCost { get; set; }
            public string projectCreateBy { get; set; }
            public string projectCreateDate { get; set; }
            public string projectEditBy { get; set; }
            public string projectEditDate { get; set; }
            public string customerName { get; set; }
            public string customerTel { get; set; }
            public object actualStart { get; set; }
            public object actualEnd { get; set; }
            public string note { get; set; }
            public object variant { get; set; }
            public object projectStatus { get; set; }

        public string picture { get; set; }

        public string backclr { get; set; }

    }

        public  class AProjectLists
        {
            public List<AProjectList> aProjectLists { get; set; }
        }
    
}
