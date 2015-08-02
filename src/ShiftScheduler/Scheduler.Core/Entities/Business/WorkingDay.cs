using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Entities.Business
{
    public class WorkingDay
    {
        public int Id { get; set; }
        public Shifts Shifts { get; set; }
        public int Week { get; set; }

        public WorkingDay()
        {
            this.Shifts = new Shifts();
        }
    }
}
