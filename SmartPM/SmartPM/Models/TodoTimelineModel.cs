using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SmartPM.Models
{
    public class TodoTimelineModel
    {
        private TodoTimelineModel todoItem;

        public TodoTimelineModel(TodoTimelineModel todoItem)
        {
            this.todoItem = todoItem;
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }


        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }



        [JsonProperty(PropertyName = "notes")]
        public string Notes { get; set; }


        [JsonProperty(PropertyName = "done")]
        public bool Done { get; set; }
    }
}
