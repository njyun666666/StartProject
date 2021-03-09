using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartProject.Models.Todo
{
    public class TodoTaskAddModel
    {
        public string subject { get; set; }
        public int state { get; set; }
        public string level { get; set; }
        public string[] tags { get; set; }
    }
}
