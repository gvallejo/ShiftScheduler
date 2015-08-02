using Scheduler.Core.BusinessRules;
using Scheduler.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Scheduling
{
    public interface ISchedulingAlgorithmFactory
    {
        IShiftScheduleValidator ScheduleValidator { get; }
        IShiftRuleFactory ShiftRuleFactory { get; set; }

        ISchedulingAlgorithm GetSchedulingAlgorithm(string name);
    }
}
