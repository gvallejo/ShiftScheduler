using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Entities
{
    public interface IWeekEmployeeScheduleItem
    {
        List<int> Days { get; set; }
        int EmployeeId { get; set; }
        int Week { get; set; }
    }
}
