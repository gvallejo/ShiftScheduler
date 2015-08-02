using Scheduler.Core.BusinessRules;
using Scheduler.Core.Entities;
using Scheduler.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Scheduling
{
    public interface ISchedulingAlgorithm
    {
        string Name { get; set; }
        IShiftScheduleValidator ScheduleValidator { get; }
        IShiftRuleFactory ShiftRuleFactory { get; set; }

        ShiftSchedule CalculateShiftSchedule(List<int> weeks);

    }
}
