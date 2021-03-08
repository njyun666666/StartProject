using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StartProject.Models.Todo
{
    public class TodoTaskModel
    {
        public int id { get; set; }
        public string subject { get; set; }
        public int state { get; set; }
        public string level { get; set; }

        [JsonIgnore]
        public string tags_string { get; set; }

        public string[] tags 
        {
            get
            {
                if(!string.IsNullOrWhiteSpace(tags_string))
                {
                    return tags_string.Split(',');
                }
                return new string[0];
            }
        }

        public DateTime ExpectDate { get; set; }
        public DateTime FinishedDate { get; set; }

    }


}
