using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Giusti.Template.Model.Results
{
    public class Messages
    {
        public List<string> Description { get; set; }

        public Messages()
        {
            Description = new List<string>();
        }
    }
}
