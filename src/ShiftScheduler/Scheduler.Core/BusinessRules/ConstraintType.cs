using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.BusinessRules
{
    public enum ConstraintType: int
    {
        Undefined,
        ShiftRuleValue,
        TimeOffRequest
    }
}
