using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Entities.Business
{
    public class TimeOffRequest
    {
        public Employee Employee { get; set; }
        public Week WeekDetails { get; set; }
    }
}
