using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Entities
{
    public class DTimeOffRequest
    {        
        public int employee_id { get; set; }
        public int week { get; set; }
        public List<int> days { get; set; }
    }
}
