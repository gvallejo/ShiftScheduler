using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Entities
{
    public class ShiftScheduleItem
    {
        public int week { get; set; }
        public List<EmployeeSchedule> schedules { get; set; }
        

        public ShiftScheduleItem()
        {
            this.schedules = new List<EmployeeSchedule>();
        }
       

 
    }
}
